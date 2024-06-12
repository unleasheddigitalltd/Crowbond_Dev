using Crowbond.Modules.WMS.Domain.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task = Crowbond.Modules.WMS.Domain.Tasks.Task;

namespace Crowbond.Modules.WMS.Infrastructure.Tasks;

internal sealed class TaskConfiguration : IEntityTypeConfiguration<Task>
{
    public void Configure(EntityTypeBuilder<Task> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.TaskTypeName).IsRequired().HasMaxLength(100);

        builder.HasOne<TaskType>().WithMany().HasForeignKey(t => t.TaskTypeName);
    }
}
