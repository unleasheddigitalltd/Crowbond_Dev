using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Infrastructure.Outbox;
using Crowbond.Modules.WMS.Infrastructure.Database;
using Crowbond.Modules.WMS.Infrastructure.Inbox;
using Crowbond.Modules.WMS.Infrastructure.Outbox;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Crowbond.Common.Presentation.Endpoints;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Crowbond.Modules.WMS.Application;
using Crowbond.Modules.WMS.Application.Abstractions.Data;
using Crowbond.Modules.WMS.Domain.Products;
using Crowbond.Modules.WMS.Infrastructure.Products;
using Crowbond.Modules.WMS.Domain.Stocks;
using Crowbond.Modules.WMS.Infrastructure.Stocks;
using Crowbond.Modules.WMS.Domain.Locations;
using Crowbond.Modules.WMS.Infrastructure.Locations;
using Crowbond.Modules.WMS.Domain.Settings;
using Crowbond.Modules.WMS.Infrastructure.Settings;
using Crowbond.Modules.WMS.Domain.Receipts;
using Crowbond.Modules.WMS.Infrastructure.Receipts;
using Crowbond.Modules.OMS.IntegrationEvents;
using Crowbond.Common.Infrastructure.SoftDelete;
using Crowbond.Modules.WMS.Infrastructure.WarehouseOperators;
using Crowbond.Modules.WMS.Domain.WarehouseOperators;
using Crowbond.Modules.WMS.Domain.Tasks;
using Crowbond.Modules.WMS.Infrastructure.Tasks;
using Crowbond.Common.Infrastructure.ChangeDetection;
using Crowbond.Modules.WMS.Application.Abstractions.Authentication;
using Crowbond.Modules.WMS.Infrastructure.Authentication;
using Crowbond.Common.Infrastructure.AuditEntity;
using Crowbond.Common.Infrastructure.TrackEntityChange;
using Crowbond.Modules.WMS.Domain.Dispatches;
using Crowbond.Modules.WMS.Infrastructure.Dispatches;
using Crowbond.Modules.WMS.PublicApi;
using Crowbond.Modules.WMS.Infrastructure.PublicApi;
using Crowbond.Modules.WMS.Domain.Users;
using Crowbond.Modules.WMS.Infrastructure.Users;
using Crowbond.Modules.Users.IntegrationEvents;
using Crowbond.Modules.WMS.Domain.Reports;
using Crowbond.Modules.WMS.Application.Tasks;
using Crowbond.Modules.WMS.Application.Tasks.Picking.Strategies;

namespace Crowbond.Modules.WMS.Infrastructure;

public static class WmsModule
{
    public static IServiceCollection AddWMSModule(
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
        registrationConfigurator.AddConsumer<IntegrationEventConsumer<PurchaseOrderApprovedIntegrationEvent>>();
        registrationConfigurator.AddConsumer<IntegrationEventConsumer<PurchaseOrderCancelledIntegrationEvent>>();
        registrationConfigurator.AddConsumer<IntegrationEventConsumer<RouteTripApprovedIntegrationEvent>>();
        registrationConfigurator.AddConsumer<IntegrationEventConsumer<OrderAcceptedIntegrationEvent>>();
        registrationConfigurator.AddConsumer<IntegrationEventConsumer<UserRegisteredIntegrationEvent>>();
        registrationConfigurator.AddConsumer<IntegrationEventConsumer<UserProfileUpdatedIntegrationEvent>>();
        registrationConfigurator.AddConsumer<IntegrationEventConsumer<UserActivatedIntegrationEvent>>();
        registrationConfigurator.AddConsumer<IntegrationEventConsumer<UserDeactivatedIntegrationEvent>>();
        registrationConfigurator.AddConsumer<IntegrationEventConsumer<WarehouseOperatorRoleAddedIntegrationEvent>>();
        registrationConfigurator.AddConsumer<IntegrationEventConsumer<WarehouseOperatorRoleRemovedIntegrationEvent>>();
    }

    private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<WmsDbContext>((sp, options) =>
            options
                .UseNpgsql(
                    configuration.GetConnectionString("Database"),
                    npgsqlOptions => npgsqlOptions
                        .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.WMS))
                .AddInterceptors(sp.GetRequiredService<InsertOutboxMessagesInterceptor>())
                .AddInterceptors(sp.GetRequiredService<ChangeDetectionInterceptor>())
                .AddInterceptors(sp.GetRequiredService<SoftDeleteInterceptor>())
                .AddInterceptors(sp.GetRequiredService<TrackEntityChangeInterceptor>())
                .AddInterceptors(sp.GetRequiredService<AuditEntityInterceptor>())
                .UseSnakeCaseNamingConvention());

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<WmsDbContext>());

        services.AddScoped<IProductRepository, ProductRepository>();

        services.AddScoped<IStockRepository, StockRepository>();

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<ILocationRepository, LocationRepository>();

        services.AddScoped<ISettingRepository, SettingRepository>();

        services.AddScoped<IReceiptRepository, ReceiptRepository>();

        services.AddScoped<IWarehouseOperatorRepository, WarehouseOperatorRepository> ();

        services.AddScoped<ITaskRepository, TaskRepository>();

        services.AddScoped<IDispatchRepository, DispatchRepository>();
        
        services.AddScoped<ConsolidatedPickingTaskService>();
        
        // Register picking strategies
        services.AddScoped<IProductPickingClassifier, NameBasedProductPickingClassifier>();
        services.AddScoped<ConsolidatedPickingStrategy>();
        services.AddScoped<IndividualPickingStrategy>();

        services.AddScoped<IWarehouseOperatorContext, WarehouseOperatorContext>();

        services.AddScoped<IStockApi, StockApi>();
        services.AddScoped<IWarehouseOperatorApi, WarehouseOperatorApi>();

        services.Configure<OutboxOptions>(configuration.GetSection("WMS:Outbox"));

        services.ConfigureOptions<ConfigureProcessOutboxJob>();

        services.Configure<InboxOptions>(configuration.GetSection("WMS:Inbox"));

        services.ConfigureOptions<ConfigureProcessInboxJob>();

        services.AddScoped<IReportService, ReportService>();
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
