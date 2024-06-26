﻿using Crowbond.Modules.WMS.Domain.Receipts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.WMS.Infrastructure.Receipts;

internal sealed class ReceiptHeaderConfiguration : IEntityTypeConfiguration<ReceiptHeader>
{
    public void Configure(EntityTypeBuilder<ReceiptHeader> builder)
    {
        builder.HasKey(r => r.Id);
        builder.Property(r => r.DeliveryNoteNumber).IsRequired().HasMaxLength(255);

    }
}
