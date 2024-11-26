using Crowbond.Common.Infrastructure.Inbox;
using Crowbond.Common.Infrastructure.Outbox;
using Crowbond.Modules.OMS.Domain.Sequences;
using Crowbond.Modules.OMS.Application.Abstractions.Data;
using Crowbond.Modules.OMS.Domain.Drivers;
using Crowbond.Modules.OMS.Domain.Orders;
using Crowbond.Modules.OMS.Domain.Routes;
using Crowbond.Modules.OMS.Infrastructure.Drivers;
using Crowbond.Modules.OMS.Infrastructure.Orders;
using Crowbond.Modules.OMS.Infrastructure.Routes;
using Crowbond.Modules.OMS.Infrastructure.Sequences;
using Microsoft.EntityFrameworkCore;
using Crowbond.Modules.OMS.Domain.RouteTrips;
using Crowbond.Modules.OMS.Infrastructure.RouteTrips;
using Crowbond.Modules.OMS.Domain.RouteTripLogs;
using Crowbond.Modules.OMS.Infrastructure.RouteTripLogs;
using Crowbond.Modules.OMS.Infrastructure.PurchaseOrders;
using Crowbond.Modules.OMS.Domain.PurchaseOrders;
using Crowbond.Common.Infrastructure.Configuration;
using Crowbond.Modules.OMS.Domain.Settings;
using Crowbond.Modules.OMS.Infrastructure.Settings;
using Crowbond.Modules.OMS.Domain.Vehicles;
using Crowbond.Modules.OMS.Infrastructure.Vehicles;
using Crowbond.Modules.OMS.Domain.Compliances;
using Crowbond.Modules.OMS.Infrastructure.Compliances;

namespace Crowbond.Modules.OMS.Infrastructure.Database;

public sealed class OmsDbContext(DbContextOptions<OmsDbContext> options) : DbContext(options), IUnitOfWork
{
    internal DbSet<Sequence> Sequences { get; set; }
    internal DbSet<OrderHeader> OrderHeaders {  get; set; }
    internal DbSet<OrderLine> OrderLines {  get; set; }
    internal DbSet<OrderLineReject> OrderLineRejects {  get; set; }
    internal DbSet<OrderLineRejectReason> OrderLineRejectReasons {  get; set; }
    internal DbSet<PurchaseOrderLine> PurchaseOrderLines { get; set; }
    internal DbSet<PurchaseOrderHeader> PurchaseOrderHeaders { get; set; }
    internal DbSet<Driver> Drivers { get; set; }
    internal DbSet<Vehicle> Vehicles{ get; set; }
    internal DbSet<Route> Routes { get; set; }
    internal DbSet<RouteTrip> RouteTrips { get; set; }
    internal DbSet<RouteTripStatusHistory> RouteTripStatusHistories { get; set; }
    internal DbSet<RouteTripLog> RouteTripLogs { get; set; }
    internal DbSet<OrderDelivery> OrderDeliveries { get; set; }
    internal DbSet<OrderDeliveryImage> OrderDeliveryImages { get; set; }
    internal DbSet<OrderStatusHistory> OrderStatusHistories { get; set; }
    internal DbSet<PurchaseOrderStatusHistory> PurchaseOrderStatusHistories { get; set; }
    internal DbSet<Setting> Settings { get; set; }
    internal DbSet<ComplianceHeader> ComplianceHeaders { get; set; }
    internal DbSet<ComplianceLine> ComplianceLines { get; set; }
    internal DbSet<ComplianceQuestion> ComplianceQuestions { get; set; }
    internal DbSet<ComplianceLineImage> ComplianceLineImages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.OMS);

        modelBuilder.ApplySoftDeleteFilter();

        modelBuilder.ApplyConfiguration(new OutboxMessageConfiguration());
        modelBuilder.ApplyConfiguration(new OutboxMessageConsumerConfiguration());
        modelBuilder.ApplyConfiguration(new InboxMessageConfiguration());
        modelBuilder.ApplyConfiguration(new InboxMessageConsumerConfiguration());
        modelBuilder.ApplyConfiguration(new SequenceConfiguration());
        modelBuilder.ApplyConfiguration(new PurchaseOrderHeaderConfiguratin());
        modelBuilder.ApplyConfiguration(new PurchaseOrderLineConfiguration());
        modelBuilder.ApplyConfiguration(new OrderHeaderConfiguratin());
        modelBuilder.ApplyConfiguration(new OrderLineConfiguration());
        modelBuilder.ApplyConfiguration(new OrderLineRejectConfiguration());
        modelBuilder.ApplyConfiguration(new OrderLineRejectReasonConfiguration());
        modelBuilder.ApplyConfiguration(new DriverConfiguration());
        modelBuilder.ApplyConfiguration(new VehicleConfiguration());
        modelBuilder.ApplyConfiguration(new RouteConfiguration());
        modelBuilder.ApplyConfiguration(new RouteTripConfiguration());
        modelBuilder.ApplyConfiguration(new RouteTripStatusHistoryConfiguration());
        modelBuilder.ApplyConfiguration(new RouteTripLogConfiguration());
        modelBuilder.ApplyConfiguration(new OrderDeliveryConfiguration());
        modelBuilder.ApplyConfiguration(new OrderDeliveryImageConfiguration());
        modelBuilder.ApplyConfiguration(new OrderStatusHistoryConfiguration());
        modelBuilder.ApplyConfiguration(new PurchaseOrderStatusHistoryConfiguration());
        modelBuilder.ApplyConfiguration(new SettingConfiguration());
        modelBuilder.ApplyConfiguration(new ComplianceHeaderConfiguration());
        modelBuilder.ApplyConfiguration(new ComplianceLineConfiguration());
        modelBuilder.ApplyConfiguration(new ComplianceQuestionConfiguration());
        modelBuilder.ApplyConfiguration(new ComplianceLineImageConfiguration());
    }
}
