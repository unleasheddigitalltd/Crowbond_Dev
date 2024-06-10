using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using Crowbond.Common.Infrastructure.Inbox;
using Crowbond.Common.Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore.Storage;
using Crowbond.Modules.Warehouse.Application.Abstractions.Data;
using Crowbond.Modules.Warehouse.Infrastructure.Locations;
using Crowbond.Modules.Warehouse.Infrastructure.Receipts;
using Crowbond.Modules.Warehouse.Infrastructure.Stocks;
using Crowbond.Modules.Warehouse.Infrastructure.Tasks;
using Crowbond.Modules.Warehouse.Domain.Locations;
using Crowbond.Modules.Warehouse.Domain.Receipts;
using Crowbond.Modules.Warehouse.Domain.Stocks;
using Task = Crowbond.Modules.Warehouse.Domain.Tasks.Task;
using Crowbond.Modules.Warehouse.Domain.Tasks;

namespace Crowbond.Modules.Warehouse.Infrastructure.Database;

public sealed class WarehouseDbContext(DbContextOptions<WarehouseDbContext> options)
    : DbContext(options), IUnitOfWork
{
    internal DbSet<Location> Locations { get; set; }

    internal DbSet<ReceiptHeader> ReceiptHeaders { get; set; }

    internal DbSet<ReceiptLine> ReceiptLines { get; set; }

    internal DbSet<ActionType> ActionTypes { get; set; }

    internal DbSet<Stock> Stocks { get; set; }

    internal DbSet<StockTransaction> StockTransactions { get; set; }

    internal DbSet<Task> Tasks { get; set; }

    internal DbSet<TaskType> TaskTypes { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Warehouse);

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
        modelBuilder.ApplyConfiguration(new TaskConfiguration());
        modelBuilder.ApplyConfiguration(new TaskTypeConfiguration());
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
