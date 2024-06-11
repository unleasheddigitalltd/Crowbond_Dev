using Crowbond.Modules.Warehouse.Domain.Stocks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task = Crowbond.Modules.Warehouse.Domain.Tasks.Task;

namespace Crowbond.Modules.Warehouse.Infrastructure.Stocks;

internal sealed class StockTransactionConfiguration : IEntityTypeConfiguration<StockTransaction>
{
    public void Configure(EntityTypeBuilder<StockTransaction> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.ActionTypeName).IsRequired().HasMaxLength(100);
        builder.Property(t => t.TransactionNote).IsRequired(false).HasMaxLength(255);
        builder.Property(t => t.Quantity).IsRequired().HasPrecision(10, 2);

        builder.HasOne<Stock>().WithMany().HasForeignKey(t => t.StockId);
        builder.HasOne<ActionType>().WithMany().HasForeignKey(t => t.ActionTypeName);
        builder.HasOne<Task>().WithMany().HasForeignKey(t => t.TaskId);
    }
}
