using Crowbond.Modules.Warehouse.Domain.Receipts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.Warehouse.Infrastructure.Receipts;

internal sealed class ReceiptHeaderConfiguration : IEntityTypeConfiguration<ReceiptHeader>
{
    public void Configure(EntityTypeBuilder<ReceiptHeader> builder)
    {
        builder.HasKey(r => r.Id);
        builder.Property(r => r.DeivaryNoteNumber).IsRequired().HasMaxLength(255);

    }
}
