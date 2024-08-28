﻿using Crowbond.Modules.WMS.Domain.Locations;
using Crowbond.Modules.WMS.Domain.Products;
using Crowbond.Modules.WMS.Domain.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.WMS.Infrastructure.Tasks;

internal sealed class TaskAssignmentLineConfiguration : IEntityTypeConfiguration<TaskAssignmentLine>
{
    public void Configure(EntityTypeBuilder<TaskAssignmentLine> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.StartDateTime).IsRequired(false);
        builder.Property(t => t.EndDateTime).IsRequired(false);
        builder.Property(t => t.ToLocationId).IsRequired(false);
        builder.Property(t => t.RequestedQty).IsRequired().HasPrecision(10, 2);
        builder.Property(t => t.CompletedQty).IsRequired(false).HasPrecision(10, 2);
        builder.Property(t => t.MissedQty).IsRequired(false).HasPrecision(10, 2);

        builder.HasOne<TaskAssignment>().WithMany().HasForeignKey(t => t.TaskAssignmentId);
        builder.HasOne<Location>().WithMany().HasForeignKey(t => t.FromLocationId);
        builder.HasOne<Location>().WithMany().HasForeignKey(t => t.ToLocationId);
        builder.HasOne<Product>().WithMany().HasForeignKey(t => t.ProductId);
    }
}