﻿using Crowbond.Modules.OMS.Application.Abstractions.Data;
using Microsoft.EntityFrameworkCore;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Modules.OMS.Application;
using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Infrastructure.Outbox;
using Crowbond.Modules.OMS.Infrastructure.Database;
using Crowbond.Modules.OMS.Infrastructure.Inbox;
using Crowbond.Modules.OMS.Infrastructure.Outbox;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.EntityFrameworkCore.Migrations;
using Crowbond.Modules.OMS.Domain.Orders;
using Crowbond.Modules.OMS.Infrastructure.Orders;
using Crowbond.Modules.OMS.Domain.Drivers;
using Crowbond.Modules.OMS.Infrastructure.Drivers;
using Crowbond.Modules.OMS.Infrastructure.Routes;
using Crowbond.Modules.OMS.Domain.Routes;
using Crowbond.Modules.OMS.Domain.RouteTrips;
using Crowbond.Modules.OMS.Infrastructure.RouteTrips;
using Crowbond.Modules.OMS.Domain.RouteTripLogs;
using Crowbond.Modules.OMS.Infrastructure.RouteTripLogs;
using Crowbond.Modules.OMS.Domain.RouteTripLogDatails;
using Crowbond.Modules.OMS.Infrastructure.RouteTripLogDatails;
using Crowbond.Modules.OMS.Domain.Deliveries;
using Crowbond.Modules.OMS.Infrastructure.Deliveries;
using Crowbond.Modules.OMS.Domain.DeliveryImages;
using Crowbond.Modules.OMS.Infrastructure.DeliveryImages;
using Crowbond.Modules.OMS.Infrastructure.PurchaseOrderHeaders;
using Crowbond.Modules.OMS.Domain.PurchaseOrders;
using Crowbond.Common.Infrastructure.ChangeDetection;
using Crowbond.Common.Infrastructure.SoftDelete;
using Crowbond.Modules.OMS.Infrastructure.Authentication;
using Crowbond.Modules.OMS.Application.Abstractions.Authentication;
using Crowbond.Common.Infrastructure.TrackEntityChange;
using Crowbond.Common.Infrastructure.AuditEntity;

namespace Crowbond.Modules.OMS.Infrastructure;

public static class OmsModule
{
    public static IServiceCollection AddOMSModule(
            this IServiceCollection services,
            IConfiguration configuration)
    {
        services.AddDomainEventHandlers();

        services.AddIntegrationEventHandlers();

        services.AddInfrastructure(configuration);

        services.AddEndpoints(Presentation.AssemblyReference.Assembly);

        return services;
    }

    private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<OmsDbContext>((sp, options) =>
            options
                .UseNpgsql(
                    configuration.GetConnectionString("Database"),
                    npgsqlOptions => npgsqlOptions
                        .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.OMS))
                .AddInterceptors(sp.GetRequiredService<InsertOutboxMessagesInterceptor>())
                .AddInterceptors(sp.GetRequiredService<ChangeDetectionInterceptor>())
                .AddInterceptors(sp.GetRequiredService<SoftDeleteInterceptor>())
                .AddInterceptors(sp.GetRequiredService<TrackEntityChangeInterceptor>())
                .AddInterceptors(sp.GetRequiredService<AuditEntityInterceptor>())
                .UseSnakeCaseNamingConvention());

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<OmsDbContext>());

        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IPurchaseOrderRepository, PurchaseOrderRepository>();
        services.AddScoped<IDriverRepository, DriverRepository>();
        services.AddScoped<IRouteRepository, RouteRepository>();
        services.AddScoped<IRouteTripRepository, RouteTripRepository>();
        services.AddScoped<IRouteTripStatusHistoryRepository, RouteTripStatusHistoryRepository>();
        services.AddScoped<IRouteTripLogRepository, RouteTripLogRepository>();
        services.AddScoped<IRouteTripLogDatailRepository, RouteTripLogDatailRepository>();
        services.AddScoped<IDeliveryRepository, DeliveryRepository>();
        services.AddScoped<IDeliveryImageRepository, DeliveryImageRepository>();
        services.AddScoped<IOrderStatusHistoryRepository, OrderStatusHistoryRepository>();

        services.AddScoped<IDriverContext, DriverContext>();

        services.Configure<OutboxOptions>(configuration.GetSection("OMS:Outbox"));
        services.ConfigureOptions<ConfigureProcessOutboxJob>();

        services.Configure<InboxOptions>(configuration.GetSection("OMS:Inbox"));
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
