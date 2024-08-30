using Crowbond.Modules.WMS.Domain.Tasks;
using Crowbond.Modules.WMS.Domain.WarehouseOperators;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.WMS.Infrastructure.Tasks;

internal sealed class TaskAssignmentConfiguration : IEntityTypeConfiguration<TaskAssignment>
{
    public void Configure(EntityTypeBuilder<TaskAssignment> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.LastModifiedBy).IsRequired(false);
        builder.Property(t => t.LastModifiedDate).IsRequired(false);

        builder.HasOne<WarehouseOperator>().WithMany().HasForeignKey(t => t.AssignedOperatorId);
    }
}
