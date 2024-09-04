using Crowbond.Modules.WMS.Domain.Stocks;
using Crowbond.Modules.WMS.Domain.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.WMS.Infrastructure.Stocks;

internal sealed class StockTransactionConfiguration : IEntityTypeConfiguration<StockTransaction>
{
    public void Configure(EntityTypeBuilder<StockTransaction> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.TaskAssignmentLineId).IsRequired(false);
        builder.Property(t => t.ActionTypeName).IsRequired().HasMaxLength(100);
        builder.Property(t => t.TransactionNote).IsRequired(false).HasMaxLength(255);
        builder.Property(t => t.ReasonId).IsRequired(false);
        builder.Property(t => t.Quantity).IsRequired().HasPrecision(10, 2);

        builder.HasOne<ActionType>().WithMany().HasForeignKey(t => t.ActionTypeName);
        builder.HasOne<StockTransactionReason>().WithMany().HasForeignKey(t => t.ReasonId);
        builder.HasOne<TaskAssignmentLine>().WithMany().HasForeignKey(t => t.TaskAssignmentLineId);
    }
}
