﻿using Crowbond.Modules.Warehouse.Domain.Stocks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.Warehouse.Infrastructure.Stocks;

internal sealed class ActionTypeConfiguration : IEntityTypeConfiguration<ActionType>
{
    public void Configure(EntityTypeBuilder<ActionType> builder)
    {
        builder.HasKey(a => a.Name);
        builder.Property(a => a.Name).IsRequired().HasMaxLength(100);
    }
}
