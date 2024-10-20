﻿using Crowbond.Modules.WMS.Domain.Tasks;
using Crowbond.Modules.WMS.Domain.WarehouseOperators;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.WMS.Infrastructure.Tasks;

internal sealed class TaskAssignmentConfiguration : IEntityTypeConfiguration<TaskAssignment>
{
    public void Configure(EntityTypeBuilder<TaskAssignment> builder)
    {
        builder.HasKey(t => t.Id);

        builder.HasOne<WarehouseOperator>().WithMany().HasForeignKey(t => t.AssignedOperatorId);

        builder.HasOne(a => a.Header)
               .WithMany(t => t.Assignments)
               .HasForeignKey(a => a.TaskHeaderId)
               .IsRequired();
    }
}
