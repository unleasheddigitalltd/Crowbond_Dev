﻿using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Infrastructure.Outbox;
using Crowbond.Common.Presentation.Endpoints;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Crowbond.Modules.CRM.Application;
using Crowbond.Modules.CRM.Application.Abstractions.Data;
using Crowbond.Modules.CRM.Infrastructure.Database;
using Crowbond.Modules.CRM.Infrastructure.Outbox;
using Crowbond.Modules.CRM.Infrastructure.Inbox;
using Crowbond.Modules.CRM.Domain.Customers;
using Crowbond.Modules.CRM.Infrastructure.Customers;
using Crowbond.Modules.CRM.Domain.Suppliers;
using Crowbond.Modules.CRM.Infrastructure.Suppliers;
using Crowbond.Modules.CRM.Domain.Reps;
using Crowbond.Modules.CRM.Infrastructure.Reps;
using Crowbond.Modules.CRM.Domain.Recipients;
using Crowbond.Modules.CRM.Infrastructure.Recipients;
using Crowbond.Modules.CRM.Domain.CustomerContacts;
using Crowbond.Modules.CRM.Infrastructure.CustomerContacts;
using Crowbond.Modules.CRM.Domain.CustomerOutlets;
using Crowbond.Modules.CRM.Infrastructure.CustomerOutlets;
using Crowbond.Modules.CRM.Domain.CustomerProducts;
using Crowbond.Modules.CRM.Infrastructure.CustomerProducts;
using Crowbond.Modules.CRM.Domain.PriceTiers;
using Crowbond.Modules.CRM.Infrastructure.PriceTiers;
using Crowbond.Modules.CRM.Domain.ProductPrices;
using Crowbond.Modules.CRM.Infrastructure.ProductPrices;
using Crowbond.Modules.CRM.Domain.Routes;
using Crowbond.Modules.CRM.Infrastructure.Routes;
using Crowbond.Modules.CRM.Domain.SupplierContacts;
using Crowbond.Modules.CRM.Infrastructure.SupplierContacts;
using Crowbond.Modules.CRM.PublicApi;
using Crowbond.Modules.CRM.Infrastructure.PublicApi;
using Crowbond.Modules.CRM.Infrastructure.FileStorage;
using Crowbond.Modules.CRM.Domain.Products;
using Crowbond.Modules.CRM.Infrastructure.Products;
using Crowbond.Modules.CRM.Domain.SupplierProducts;
using Crowbond.Modules.CRM.Infrastructure.SupplierProducts;
using MassTransit;
using Crowbond.Modules.WMS.IntegrationEvents;
using Crowbond.Common.Infrastructure.ChangeDetection;
using Crowbond.Common.Infrastructure.SoftDelete;
using Crowbond.Common.Infrastructure.AuditEntity;
using Crowbond.Common.Infrastructure.TrackEntityChange;
using Crowbond.Modules.OMS.IntegrationEvents;
using Crowbond.Modules.CRM.Domain.Settings;
using Crowbond.Modules.CRM.Infrastructure.Settings;
using Crowbond.Modules.CRM.Infrastructure.Authentication;
using Crowbond.Modules.CRM.Application.Abstractions.Authentication;
using Crowbond.Modules.Users.IntegrationEvents;
using Crowbond.Modules.CRM.Domain.Users;
using Crowbond.Modules.CRM.Infrastructure.Users;
using Crowbond.Modules.CRM.Application.Routes;

namespace Crowbond.Modules.CRM.Infrastructure;
public static class CrmModule
{
    public static IServiceCollection AddCRMModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDomainEventHandlers();

        services.AddIntegrationEventHandlers();

        services.AddInfrastructure(configuration);

        services.AddEndpoints(Presentation.AssemblyReference.Assembly);

        return services;
    }

    public static void ConfigureConsumers(IRegistrationConfigurator registrationConfigurator)
    {
        registrationConfigurator.AddConsumer<IntegrationEventConsumer<ProductCreatedIntegrationEvent>>();
        registrationConfigurator.AddConsumer<IntegrationEventConsumer<ProductUpdatedIntegrationEvent>>();
        registrationConfigurator.AddConsumer<IntegrationEventConsumer<CategoryCreatedIntegrationEvent>>();
        registrationConfigurator.AddConsumer<IntegrationEventConsumer<CategoryUpdatedIntegrationEvent>>();
        registrationConfigurator.AddConsumer<IntegrationEventConsumer<BrandCreatedIntegrationEvent>>();
        registrationConfigurator.AddConsumer<IntegrationEventConsumer<BrandUpdatedIntegrationEvent>>();
        registrationConfigurator.AddConsumer<IntegrationEventConsumer<ProductGroupCreatedIntegrationEvent>>();
        registrationConfigurator.AddConsumer<IntegrationEventConsumer<ProductGroupUpdatedIntegrationEvent>>();
        registrationConfigurator.AddConsumer<IntegrationEventConsumer<RouteCreatedIntegrationEvent>>();
        registrationConfigurator.AddConsumer<IntegrationEventConsumer<RouteUpdatedIntegrationEvent>>();
        registrationConfigurator.AddConsumer<IntegrationEventConsumer<OrderLineAddedIntegrationEvent>>();
        registrationConfigurator.AddConsumer<IntegrationEventConsumer<UserRegisteredIntegrationEvent>>();
        registrationConfigurator.AddConsumer<IntegrationEventConsumer<UserProfileUpdatedIntegrationEvent>>();
        registrationConfigurator.AddConsumer<IntegrationEventConsumer<UserActivatedIntegrationEvent>>();
        registrationConfigurator.AddConsumer<IntegrationEventConsumer<UserDeactivatedIntegrationEvent>>();
    }

    private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CrmDbContext>((sp, options) =>
            options
                .UseNpgsql(
                    configuration.GetConnectionString("Database"),
                    npgsqlOptions => npgsqlOptions
                        .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.CRM))
                .AddInterceptors(sp.GetRequiredService<InsertOutboxMessagesInterceptor>())
                .AddInterceptors(sp.GetRequiredService<ChangeDetectionInterceptor>())
                .AddInterceptors(sp.GetRequiredService<SoftDeleteInterceptor>())
                .AddInterceptors(sp.GetRequiredService<TrackEntityChangeInterceptor>())
                .AddInterceptors(sp.GetRequiredService<AuditEntityInterceptor>())
                .UseSnakeCaseNamingConvention());

        services.AddScoped<ICustomerRepository,CustomerRepository>();
        services.AddScoped<ICustomerContactRepository,CustomerContactRepository>();
        services.AddScoped<ICustomerOutletRepository,CustomerOutletRepository>();
        services.AddScoped<ISupplierRepository, SupplierRepository>();
        services.AddScoped<ISupplierContactRepository, SupplierContactRepository>();
        services.AddScoped<ISupplierProductRepository, SupplierProductRepository>();
        services.AddScoped<IRepRepository, RepRepository>();
        services.AddScoped<IRecipientRepository, RecipientRepository>();
        services.AddScoped<IPriceTierRepository, PriceTierRepository>();
        services.AddScoped<IProductPriceRepository, ProductPriceRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICustomerProductRepository, CustomerProductRepository>();
        services.AddScoped<IRouteRepository, RouteRepository>();
        services.AddScoped<ISettingRepository, SettingRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddSingleton<RouteService>();

        services.AddScoped<ISupplierApi, SupplierApi>();
        services.AddScoped<ISupplierProductApi, SupplierProductsApi>();

        services.AddScoped<ICustomerApi, CustomerApi>();
        services.AddScoped<ICustomerProductApi, CustomerProductApi>();

        services.AddScoped<ICustomerFileAccess, CustomerFileAccess>();

        services.AddScoped<IContactContext, CustomerContext>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<CrmDbContext>());

        services.Configure<OutboxOptions>(configuration.GetSection("CRM:Outbox"));
        services.ConfigureOptions<ConfigureProcessOutboxJob>();

        services.Configure<FileStorageOptions>(configuration.GetSection("CRM:FileSettings"));


        services.Configure<InboxOptions>(configuration.GetSection("CRM:Inbox"));
        services.ConfigureOptions<ConfigureProcessInboxJob>();
    }

    private static void AddDomainEventHandlers(this IServiceCollection services)
    {
        Type[] domainEventHandlers = AssemblyReference.Assembly
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
        Type[] integrationEventHandlers = Presentation.AssemblyReference.Assembly
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
