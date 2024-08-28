using Crowbond.Modules.WMS.Domain.Tasks;
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

        builder.HasOne<TaskHeader>().WithMany().HasForeignKey(t => t.TaskHeaderId);
    }
}
