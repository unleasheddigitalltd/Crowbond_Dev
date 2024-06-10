using Crowbond.Modules.Warehouse.Domain.Receipts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.Warehouse.Infrastructure.Receipts;

internal sealed class ReceiptLineConfiguration : IEntityTypeConfiguration<ReceiptLine>
{
    public void Configure(EntityTypeBuilder<ReceiptLine> builder)
    {
        builder.HasKey(r => r.Id);
        builder.Property(r => r.QuantityReceived).HasPrecision(10, 2);
        builder.Property(r => r.UnitPrice).HasPrecision(10, 2);
        builder.Property(r => r.SellByDate).IsRequired(false);
        builder.Property(r => r.UseByDate).IsRequired(false);
        builder.Property(r => r.BatchNumber).IsRequired(false).HasMaxLength(255);

        builder.HasOne<ReceiptHeader>().WithMany().HasForeignKey(r => r.ReceiptHeaderId);
    }
}
