using Crowbond.Modules.WMS.Domain.Stocks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.WMS.Infrastructure.Stocks;

internal sealed class StockTransactionReasonConfiguration : IEntityTypeConfiguration<StockTransactionReason>
{
    public void Configure(EntityTypeBuilder<StockTransactionReason> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Name).IsRequired().HasMaxLength(255);
        builder.Property(t => t.ActionTypeName).IsRequired().HasMaxLength(100);

        builder.HasOne<ActionType>().WithMany().HasForeignKey(t => t.ActionTypeName);
    }
}
