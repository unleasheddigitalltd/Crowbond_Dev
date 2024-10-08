﻿using Crowbond.Modules.CRM.Domain.Sequences;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.CRM.Infrastructure.Sequences;
internal sealed class SequenceConfiguration : IEntityTypeConfiguration<Sequence>
{
    public void Configure(EntityTypeBuilder<Sequence> builder)
    {
        builder.HasKey(s => s.Context);

        builder.Property(s => s.Prefix).IsRequired().HasMaxLength(3);

        builder.HasData(Sequence.Customer, Sequence.Supplier);
    }
}
