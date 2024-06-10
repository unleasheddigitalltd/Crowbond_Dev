using Crowbond.Modules.Warehouse.Domain.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task = Crowbond.Modules.Warehouse.Domain.Tasks.Task;

namespace Crowbond.Modules.Warehouse.Infrastructure.Tasks;

internal sealed class TaskConfiguration : IEntityTypeConfiguration<Task>
{
    public void Configure(EntityTypeBuilder<Task> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.TaskTypeName).IsRequired().HasMaxLength(100);

        builder.HasOne<TaskType>().WithMany().HasForeignKey(t => t.TaskTypeName);
    }
}
