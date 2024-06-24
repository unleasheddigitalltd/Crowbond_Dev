using System.Data.Common;
using Crowbond.Common.Infrastructure.Inbox;
using Crowbond.Common.Infrastructure.Outbox;
using Crowbond.Modules.WMS.Infrastructure.Locations;
using Crowbond.Modules.WMS.Infrastructure.Receipts;
using Crowbond.Modules.WMS.Infrastructure.Stocks;
using Crowbond.Modules.WMS.Infrastructure.Tasks;
using Task = Crowbond.Modules.WMS.Domain.Tasks.Task;
using Crowbond.Modules.WMS.Infrastructure.Categories;
using Crowbond.Modules.WMS.Infrastructure.Products;
using Crowbond.Modules.WMS.Application.Abstractions.Data;
using Crowbond.Modules.WMS.Domain.Locations;
using Crowbond.Modules.WMS.Domain.Receipts;
using Crowbond.Modules.WMS.Domain.Stocks;
using Crowbond.Modules.WMS.Domain.Categories;
using Crowbond.Modules.WMS.Domain.Tasks;
using Crowbond.Modules.WMS.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Crowbond.Modules.WMS.Infrastructure.Database;

public sealed class WmsDbContext(DbContextOptions<WmsDbContext> options)
    : DbContext(options), IUnitOfWork
{
    internal DbSet<Product> Products { get; set; }

    internal DbSet<UnitOfMeasure> UnitOfMeasures { get; set; }

    internal DbSet<FilterType> FilterTypes { get; set; }

    internal DbSet<InventoryType> InventoryTypes { get; set; }

    internal DbSet<Category> Categories { get; set; }

    internal DbSet<Location> Locations { get; set; }

    internal DbSet<LocationType> LocationsType { get; set; }

    internal DbSet<ReceiptHeader> ReceiptHeaders { get; set; }

    internal DbSet<ReceiptLine> ReceiptLines { get; set; }

    internal DbSet<ActionType> ActionTypes { get; set; }

    internal DbSet<Stock> Stocks { get; set; }

    internal DbSet<StockTransaction> StockTransactions { get; set; }

    internal DbSet<StockTransactionReason> StockTransactionReasons { get; set; }

    internal DbSet<Task> Tasks { get; set; }

    internal DbSet<TaskType> TaskTypes { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.WMS);

        modelBuilder.ApplyConfiguration(new OutboxMessageConfiguration());
        modelBuilder.ApplyConfiguration(new OutboxMessageConsumerConfiguration());
        modelBuilder.ApplyConfiguration(new InboxMessageConfiguration());
        modelBuilder.ApplyConfiguration(new InboxMessageConsumerConfiguration());
        modelBuilder.ApplyConfiguration(new LocationConfiguration());
        modelBuilder.ApplyConfiguration(new LocationTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ReceiptHeaderConfiguration());
        modelBuilder.ApplyConfiguration(new ReceiptLineConfiguration());
        modelBuilder.ApplyConfiguration(new ActionTypeConfiguration());
        modelBuilder.ApplyConfiguration(new StockConfiguration());
        modelBuilder.ApplyConfiguration(new StockTransactionConfiguration());
        modelBuilder.ApplyConfiguration(new StockTransactionReasonConfiguration());
        modelBuilder.ApplyConfiguration(new TaskConfiguration());
        modelBuilder.ApplyConfiguration(new TaskTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new UnitOfMeasureConfiguration());
        modelBuilder.ApplyConfiguration(new FilterTypeConfiguration());
        modelBuilder.ApplyConfiguration(new InventoryTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
    }
    public async Task<DbTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (Database.CurrentTransaction is not null)
        {
            await Database.CurrentTransaction.DisposeAsync();
        }

        return (await Database.BeginTransactionAsync(cancellationToken)).GetDbTransaction();
    }
}
