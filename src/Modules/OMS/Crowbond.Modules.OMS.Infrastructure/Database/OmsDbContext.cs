using Crowbond.Common.Infrastructure.Inbox;
using Crowbond.Common.Infrastructure.Outbox;
using Crowbond.Modules.OMS.Domain.Sequences;
using Crowbond.Modules.OMS.Application.Abstractions.Data;
using Crowbond.Modules.OMS.Domain.Drivers;
using Crowbond.Modules.OMS.Domain.Orders;
using Crowbond.Modules.OMS.Domain.PurchaseOrders;
using Crowbond.Modules.OMS.Domain.Routes;
using Crowbond.Modules.OMS.Infrastructure.Drivers;
using Crowbond.Modules.OMS.Infrastructure.Orders;
using Crowbond.Modules.OMS.Infrastructure.PurchaseOrders;
using Crowbond.Modules.OMS.Infrastructure.Routes;
using Crowbond.Modules.OMS.Infrastructure.Sequences;
using Microsoft.EntityFrameworkCore;
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

namespace Crowbond.Modules.OMS.Infrastructure.Database;

public sealed class OmsDbContext(DbContextOptions<OmsDbContext> options) : DbContext(options), IUnitOfWork
{
    internal DbSet<Sequence> Sequences { get; set; }
    internal DbSet<OrderHeader> OrderHeaders {  get; set; }
    internal DbSet<OrderLine> OrderLines {  get; set; }
    internal DbSet<PurchaseOrderLine> PurchaseOrderLines { get; set; }
    internal DbSet<PurchaseOrderHeader> PurchaseOrderHeaders { get; set; }
    internal DbSet<Driver> Drivers { get; set; }
    internal DbSet<Route> Routes { get; set; }
    internal DbSet<RouteTrip> RouteTrips { get; set; }
    internal DbSet<RouteTripStatusHistory> RouteTripStatusHistories { get; set; }
    internal DbSet<RouteTripLog> RouteTripLogs { get; set; }
    internal DbSet<RouteTripLogDatail> RouteTripLogDatails { get; set; }
    internal DbSet<Delivery> Deliveries { get; set; }
    internal DbSet<DeliveryImage> DeliveryImages { get; set; }
    internal DbSet<OrderStatusHistory> OrderStatusHistories { get; set; }
    internal DbSet<PurchaseOrderStatusHistory> PurchaseOrderStatusHistories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.OMS);

        modelBuilder.ApplyConfiguration(new OutboxMessageConfiguration());
        modelBuilder.ApplyConfiguration(new OutboxMessageConsumerConfiguration());
        modelBuilder.ApplyConfiguration(new InboxMessageConfiguration());
        modelBuilder.ApplyConfiguration(new InboxMessageConsumerConfiguration());
        modelBuilder.ApplyConfiguration(new SequenceConfiguration());
        modelBuilder.ApplyConfiguration(new PurchaseOrderHeaderConfiguratin());
        modelBuilder.ApplyConfiguration(new PurchaseOrderLineConfiguration());
        modelBuilder.ApplyConfiguration(new OrderHeaderConfiguratin());
        modelBuilder.ApplyConfiguration(new OrderLineConfiguration());
        modelBuilder.ApplyConfiguration(new DriverConfiguration());
        modelBuilder.ApplyConfiguration(new RouteConfiguration());
        modelBuilder.ApplyConfiguration(new RouteTripConfiguration());
        modelBuilder.ApplyConfiguration(new RouteTripStatusHistoryConfiguration());
        modelBuilder.ApplyConfiguration(new RouteTripLogConfiguration());
        modelBuilder.ApplyConfiguration(new RouteTripLogDatailConfiguration());
        modelBuilder.ApplyConfiguration(new DeliveryConfiguration());
        modelBuilder.ApplyConfiguration(new DeliveryImageConfiguration());
        modelBuilder.ApplyConfiguration(new OrderStatusHistoryConfiguration());
        modelBuilder.ApplyConfiguration(new PurchaseOrderStatusHistoryConfiguration());
    }
}
