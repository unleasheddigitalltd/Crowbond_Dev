using Amazon;
using Amazon.CognitoIdentityProvider;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Crowbond.Common.Application.Authorization;
using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Application.Users;
using Crowbond.Common.Infrastructure.AuditEntity;
using Crowbond.Common.Infrastructure.ChangeDetection;
using Crowbond.Common.Infrastructure.Outbox;
using Crowbond.Common.Infrastructure.SoftDelete;
using Crowbond.Common.Infrastructure.TrackEntityChange;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Modules.CRM.IntegrationEvents;
using Crowbond.Modules.Users.Application.Abstractions.Data;
using Crowbond.Modules.Users.Application.Abstractions.Identity;
using Crowbond.Modules.Users.Domain.Users;
using Crowbond.Modules.Users.Infrastructure.Authorization;
using Crowbond.Modules.Users.Infrastructure.Database;
using Crowbond.Modules.Users.Infrastructure.Identity;
using Crowbond.Modules.Users.Infrastructure.Inbox;
using Crowbond.Modules.Users.Infrastructure.Outbox;
using Crowbond.Modules.Users.Infrastructure.Users;
using Crowbond.Modules.Users.Presentation;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace Crowbond.Modules.Users.Infrastructure;

public static class UsersModule
{
    public static IServiceCollection AddUsersModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDomainEventHandlers();

        services.AddIntegrationEventHandlers();

        services.AddInfrastructure(configuration);

        services.AddEndpoints(AssemblyReference.Assembly);

        return services;
    }

    public static void ConfigureConsumers(IRegistrationConfigurator registrationConfigurator)
    {
        registrationConfigurator.AddConsumer<IntegrationEventConsumer<CustomerContactCreatedIntegrationEvent>>();
        registrationConfigurator.AddConsumer<IntegrationEventConsumer<CustomerContactUpdatedIntegrationEvent>>();
        registrationConfigurator.AddConsumer<IntegrationEventConsumer<CustomerContactActivatedIntegrationEvent>>();
        registrationConfigurator.AddConsumer<IntegrationEventConsumer<CustomerContactDeactivatedIntegrationEvent>>();
        registrationConfigurator.AddConsumer<IntegrationEventConsumer<SupplierContactCreatedIntegrationEvent>>();
        registrationConfigurator.AddConsumer<IntegrationEventConsumer<SupplierContactUpdatedIntegrationEvent>>();
        registrationConfigurator.AddConsumer<IntegrationEventConsumer<SupplierContactActivatedIntegrationEvent>>();
        registrationConfigurator.AddConsumer<IntegrationEventConsumer<SupplierContactDeactivatedIntegrationEvent>>();
    }

    private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IPermissionService, PermissionService>();
        services.AddScoped<IUserService, UserService>();
        
        // Configure Cognito
        services.Configure<CognitoOptions>(configuration.GetSection("Users:Cognito"));

        // Register AWS Cognito client as singleton
        services.AddSingleton<IAmazonCognitoIdentityProvider>(sp =>
        {
            var options = sp.GetRequiredService<IOptions<CognitoOptions>>().Value;
            var region = RegionEndpoint.GetBySystemName(options.Region);
            var config = new AmazonCognitoIdentityProviderConfig
            {
                RegionEndpoint = region,
                MaxErrorRetry = 3,
                Timeout = TimeSpan.FromSeconds(10)
            };

            // Try to get credentials from profile first
            var chain = new CredentialProfileStoreChain();
            if (chain.TryGetAWSCredentials("crowbond-dev", out var credentials))
            {
                return new AmazonCognitoIdentityProviderClient(credentials, config);
            }

            // If profile not found, fall back to environment variables or instance profile
            return new AmazonCognitoIdentityProviderClient(config);
        });

        // Register the identity provider service as singleton to match AWS client lifetime
        services.AddSingleton<IIdentityProviderService, CognitoIdentityProviderService>();

        services.AddDbContext<UsersDbContext>((sp, options) =>
            options
                .UseNpgsql(
                    configuration.GetConnectionString("Database"),
                    npgsqlOptions => npgsqlOptions
                        .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Users))
                .AddInterceptors(sp.GetRequiredService<InsertOutboxMessagesInterceptor>())
                .AddInterceptors(sp.GetRequiredService<ChangeDetectionInterceptor>())
                .AddInterceptors(sp.GetRequiredService<SoftDeleteInterceptor>())
                .AddInterceptors(sp.GetRequiredService<TrackEntityChangeInterceptor>())
                .AddInterceptors(sp.GetRequiredService<AuditEntityInterceptor>())
                .UseSnakeCaseNamingConvention());

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<UsersDbContext>());

        services.Configure<OutboxOptions>(configuration.GetSection("Users:Outbox"));

        services.ConfigureOptions<ConfigureProcessOutboxJob>();

        services.Configure<InboxOptions>(configuration.GetSection("Users:Inbox"));

        services.ConfigureOptions<ConfigureProcessInboxJob>();

    }

    private static void AddDomainEventHandlers(this IServiceCollection services)
    {
        Type[] domainEventHandlers = Application.AssemblyReference.Assembly
            .GetTypes()
            .Where(t => t.IsAssignableTo(typeof(IDomainEventHandler)))
            .ToArray();

        foreach (Type domainEventHandler in domainEventHandlers)
        {
            services.TryAddScoped(domainEventHandler);

            Type domainEvent = domainEventHandler
                .GetInterfaces()
                .Single(i => i.IsGenericType)
                .GetGenericArguments()
                .Single();

            Type closedIdempotentHandler = typeof(IdempotentDomainEventHandler<>).MakeGenericType(domainEvent);

            services.Decorate(domainEventHandler, closedIdempotentHandler);
        }
    }

    private static void AddIntegrationEventHandlers(this IServiceCollection services)
    {
        Type[] integrationEventHandlers = AssemblyReference.Assembly
            .GetTypes()
            .Where(t => t.IsAssignableTo(typeof(IIntegrationEventHandler)))
            .ToArray();

        foreach (Type integrationEventHandler in integrationEventHandlers)
        {
            services.TryAddScoped(integrationEventHandler);

            Type integrationEvent = integrationEventHandler
                .GetInterfaces()
                .Single(i => i.IsGenericType)
                .GetGenericArguments()
                .Single();

            Type closedIdempotentHandler =
                typeof(IdempotentIntegrationEventHandler<>).MakeGenericType(integrationEvent);

            services.Decorate(integrationEventHandler, closedIdempotentHandler);
        }
    }
}
