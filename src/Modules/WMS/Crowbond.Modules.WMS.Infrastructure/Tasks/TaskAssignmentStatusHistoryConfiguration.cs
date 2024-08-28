using Crowbond.Modules.WMS.Domain.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.WMS.Infrastructure.Tasks;

internal sealed class TaskAssignmentStatusHistoryConfiguration : IEntityTypeConfiguration<TaskAssignmentStatusHistory>
{
    public void Configure(EntityTypeBuilder<TaskAssignmentStatusHistory> builder)
    {
        builder.HasKey(t => t.Id);

        builder.HasOne<TaskAssignment>().WithMany().HasForeignKey(t => t.TaskAssignmentId);
    }
}
