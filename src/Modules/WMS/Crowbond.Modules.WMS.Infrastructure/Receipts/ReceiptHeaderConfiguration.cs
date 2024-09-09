using Crowbond.Modules.WMS.Domain.Receipts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.WMS.Infrastructure.Receipts;

internal sealed class ReceiptHeaderConfiguration : IEntityTypeConfiguration<ReceiptHeader>
{
    public void Configure(EntityTypeBuilder<ReceiptHeader> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.ReceiptNo).IsRequired().HasMaxLength(20);
        builder.Property(r => r.DeliveryNoteNumber).IsRequired().HasMaxLength(255);
        builder.Property(r => r.PurchaseOrderId).IsRequired(false);
        builder.Property(r => r.PurchaseOrderNo).IsRequired(false).HasMaxLength(20);
    }
}
