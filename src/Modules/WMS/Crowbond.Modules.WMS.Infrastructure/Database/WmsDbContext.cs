using Microsoft.EntityFrameworkCore;
using Crowbond.Common.Infrastructure.Inbox;
using Crowbond.Common.Infrastructure.Outbox;
using Crowbond.Modules.WMS.Infrastructure.Locations;
using Crowbond.Modules.WMS.Infrastructure.Receipts;
using Crowbond.Modules.WMS.Infrastructure.Stocks;
using Crowbond.Modules.WMS.Infrastructure.Products;
using Crowbond.Modules.WMS.Application.Abstractions.Data;
using Crowbond.Modules.WMS.Domain.Locations;
using Crowbond.Modules.WMS.Domain.Receipts;
using Crowbond.Modules.WMS.Domain.Stocks;
using Crowbond.Modules.WMS.Domain.Products;
using Crowbond.Modules.WMS.Domain.Settings;
using Crowbond.Modules.WMS.Infrastructure.Settings;
using Crowbond.Modules.WMS.Infrastructure.Sequences;
using Crowbond.Modules.WMS.Domain.Sequences;
using Crowbond.Modules.WMS.Infrastructure.Tasks;
using Crowbond.Modules.WMS.Domain.WarehouseOperators;
using Crowbond.Modules.WMS.Infrastructure.WarehouseOperators;
using Crowbond.Modules.WMS.Domain.Tasks;
using Crowbond.Common.Infrastructure.Configuration;
using Crowbond.Modules.WMS.Domain.Dispatches;
using Crowbond.Modules.WMS.Infrastructure.Dispatches;
using Crowbond.Modules.WMS.Domain.Users;
using Crowbond.Modules.WMS.Infrastructure.Users;

namespace Crowbond.Modules.WMS.Infrastructure.Database;

public sealed class WmsDbContext(DbContextOptions<WmsDbContext> options)
    : DbContext(options), IUnitOfWork
{
    internal DbSet<Product> Products { get; set; }
    internal DbSet<UnitOfMeasure> UnitOfMeasures { get; set; }
    internal DbSet<FilterType> FilterTypes { get; set; }
    internal DbSet<InventoryType> InventoryTypes { get; set; }
    internal DbSet<Category> Categories { get; set; }
    internal DbSet<Brand> Brands { get; set; }
    internal DbSet<ProductGroup> ProductGroups { get; set; }
    internal DbSet<Location> Locations { get; set; }
    internal DbSet<ReceiptHeader> ReceiptHeaders { get; set; }
    internal DbSet<ReceiptLine> ReceiptLines { get; set; }
    internal DbSet<ActionType> ActionTypes { get; set; }
    internal DbSet<Stock> Stocks { get; set; }
    internal DbSet<StockTransaction> StockTransactions { get; set; }
    internal DbSet<StockTransactionReason> StockTransactionReasons { get; set; }
    internal DbSet<Setting> Settings { get; set; }
    internal DbSet<Sequence> Sequences { get; set; }
    internal DbSet<WarehouseOperator> WarehouseOperators { get; set; }
    internal DbSet<TaskHeader> TaskHeaders { get; set; }
    internal DbSet<TaskAssignment> TaskAssignments { get; set; }
    internal DbSet<TaskAssignmentLine> TaskAssignmentLines { get; set; }
    internal DbSet<TaskAssignmentStatusHistory> TaskAssignmentStatusHistories { get; set; }
    internal DbSet<DispatchHeader> DispatchHeaders { get; set; }
    internal DbSet<DispatchLine> DispatchLines { get; set; }
    internal DbSet<User> Users { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.WMS);

        modelBuilder.ApplySoftDeleteFilter();

        modelBuilder.ApplyConfiguration(new OutboxMessageConfiguration());
        modelBuilder.ApplyConfiguration(new OutboxMessageConsumerConfiguration());
        modelBuilder.ApplyConfiguration(new InboxMessageConfiguration());
        modelBuilder.ApplyConfiguration(new InboxMessageConsumerConfiguration());

        modelBuilder.ApplyConfiguration(new LocationConfiguration());

        modelBuilder.ApplyConfiguration(new ReceiptHeaderConfiguration());
        modelBuilder.ApplyConfiguration(new ReceiptLineConfiguration());

        modelBuilder.ApplyConfiguration(new ActionTypeConfiguration());
        modelBuilder.ApplyConfiguration(new StockConfiguration());
        modelBuilder.ApplyConfiguration(new StockTransactionConfiguration());
        modelBuilder.ApplyConfiguration(new StockTransactionReasonConfiguration());

        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new UnitOfMeasureConfiguration());
        modelBuilder.ApplyConfiguration(new FilterTypeConfiguration());
        modelBuilder.ApplyConfiguration(new InventoryTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new BrandConfiguration());
        modelBuilder.ApplyConfiguration(new ProductGroupConfiguration());

        modelBuilder.ApplyConfiguration(new SettingConfiguration());
        modelBuilder.ApplyConfiguration(new SequenceConfiguration());

        modelBuilder.ApplyConfiguration(new TaskHeaderConfiguration());
        modelBuilder.ApplyConfiguration(new TaskAssignmentConfiguration());
        modelBuilder.ApplyConfiguration(new TaskAssignmentLineConfiguration());
        modelBuilder.ApplyConfiguration(new TaskAssignmentStatusHistoryConfiguration());

        modelBuilder.ApplyConfiguration(new WarehouseOperatorConfiguration());

        modelBuilder.ApplyConfiguration(new DispatchHeaderConfiguration());
        modelBuilder.ApplyConfiguration(new DispatchLineConfiguration());

        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
}
