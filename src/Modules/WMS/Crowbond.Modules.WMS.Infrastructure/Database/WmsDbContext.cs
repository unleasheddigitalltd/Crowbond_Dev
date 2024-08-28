﻿using System.Data.Common;
using Crowbond.Common.Infrastructure.Inbox;
using Crowbond.Common.Infrastructure.Outbox;
using Crowbond.Modules.WMS.Infrastructure.Locations;
using Crowbond.Modules.WMS.Infrastructure.Receipts;
using Crowbond.Modules.WMS.Infrastructure.Stocks;
using Crowbond.Modules.WMS.Infrastructure.Categories;
using Crowbond.Modules.WMS.Infrastructure.Products;
using Crowbond.Modules.WMS.Application.Abstractions.Data;
using Crowbond.Modules.WMS.Domain.Locations;
using Crowbond.Modules.WMS.Domain.Receipts;
using Crowbond.Modules.WMS.Domain.Stocks;
using Crowbond.Modules.WMS.Domain.Categories;
using Crowbond.Modules.WMS.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Crowbond.Modules.WMS.Domain.Settings;
using Crowbond.Modules.WMS.Infrastructure.Settings;
using Crowbond.Modules.WMS.Infrastructure.Sequences;
using Crowbond.Modules.WMS.Domain.Sequences;
using Crowbond.Modules.WMS.Domain.Tasks;
using Crowbond.Modules.WMS.Infrastructure.Tasks;

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
    internal DbSet<ReceiptHeader> ReceiptHeaders { get; set; }
    internal DbSet<ReceiptLine> ReceiptLines { get; set; }
    internal DbSet<ActionType> ActionTypes { get; set; }
    internal DbSet<Stock> Stocks { get; set; }
    internal DbSet<StockTransaction> StockTransactions { get; set; }
    internal DbSet<StockTransactionReason> StockTransactionReasons { get; set; }
    internal DbSet<Setting> Settings { get; set; }
    internal DbSet<Sequence> Sequences { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.WMS);

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

        modelBuilder.ApplyConfiguration(new SettingConfiguration());
        modelBuilder.ApplyConfiguration(new SequenceConfiguration());

        modelBuilder.ApplyConfiguration(new TaskHeaderConfiguration());
        modelBuilder.ApplyConfiguration(new TaskAssignmentConfiguration());
        modelBuilder.ApplyConfiguration(new TaskAssignmentLineConfiguration());
        modelBuilder.ApplyConfiguration(new TaskAssignmentStatusHistoryConfiguration());
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
