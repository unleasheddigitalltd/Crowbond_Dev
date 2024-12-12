using Crowbond.Modules.WMS.Domain.Receipts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.WMS.Infrastructure.Receipts;

internal sealed class ReceiptLineConfiguration : IEntityTypeConfiguration<ReceiptLine>
{
    public void Configure(EntityTypeBuilder<ReceiptLine> builder)
    {
        builder.HasKey(r => r.Id);
        builder.Property(r => r.ReceivedQty).HasPrecision(10, 2);
        builder.Property(r => r.StoredQty).HasPrecision(10, 2);
        builder.Property(r => r.MissedQty).HasPrecision(10, 2);
        builder.Property(r => r.UnitPrice).HasPrecision(10, 2);
        builder.Property(r => r.SellByDate).IsRequired(false);
        builder.Property(r => r.UseByDate).IsRequired(false);
        builder.Property(r => r.BatchNumber).IsRequired(false).HasMaxLength(255);
    }
}
