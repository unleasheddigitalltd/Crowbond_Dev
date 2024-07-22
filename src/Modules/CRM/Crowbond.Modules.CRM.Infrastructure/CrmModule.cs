using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Infrastructure.Outbox;
using Crowbond.Common.Presentation.Endpoints;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Crowbond.Modules.CRM.Application;
using Crowbond.Modules.CRM.Application.Abstractions.Data;
using Crowbond.Modules.CRM.IntegrationEvents;
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
using Crowbond.Modules.CRM.Domain.Products;
using Crowbond.Modules.CRM.Infrastructure.Products;
using Crowbond.Modules.CRM.Domain.Categories;
using Crowbond.Modules.CRM.Infrastructure.Categories;
using Crowbond.Modules.CRM.Domain.CustomerProductPrices;
using Crowbond.Modules.CRM.Infrastructure.CustomerProductPrices;
using Crowbond.Modules.CRM.Domain.CustomerOutletRoutes;
using Crowbond.Modules.CRM.Infrastructure.CustomerOutletRoutes;
using Crowbond.Modules.CRM.Domain.Routes;
using Crowbond.Modules.CRM.Infrastructure.Routes;

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

    private static void ConfigureConsumers(IRegistrationConfigurator registrationConfigurator)
    {
        registrationConfigurator.AddConsumer<IntegrationEventConsumer<CustomerContactCreatedIntegrationEvent>>();
    }

    private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CrmDbContext>((sp, options) =>
            options
                .UseNpgsql(
                    configuration.GetConnectionString("Database"),
                    npgsqlOptions => npgsqlOptions
                        .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.CRM))
                .UseSnakeCaseNamingConvention()
                .AddInterceptors(sp.GetRequiredService<InsertOutboxMessagesInterceptor>()));

        services.AddScoped<ICustomerRepository,CustomerRepository>();
        services.AddScoped<ICustomerContactRepository,CustomerContactRepository>();
        services.AddScoped<ICustomerOutletRepository,CustomerOutletRepository>();
        services.AddScoped<ISupplierRepository, SupplierRepository>();
        services.AddScoped<IRepRepository, RepRepository>();
        services.AddScoped<IRecipientRepository, RecipientRepository>();
        services.AddScoped<IPriceTierRepository, PriceTierRepository>();
        services.AddScoped<IProductPriceRepository, ProductPriceRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICustomerProductRepository, CustomerProductRepository>();
        services.AddScoped<ICustomerProductPriceRepository, CustomerProductPriceRepository>();
        services.AddScoped<ICustomerOutletRouteRepository, CustomerOutletRouteRepository>();
        services.AddScoped<IRouteRepository, RouteRepository>();


        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<CrmDbContext>());

        services.Configure<OutboxOptions>(configuration.GetSection("CRM:Outbox"));

        services.ConfigureOptions<ConfigureProcessOutboxJob>();

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
