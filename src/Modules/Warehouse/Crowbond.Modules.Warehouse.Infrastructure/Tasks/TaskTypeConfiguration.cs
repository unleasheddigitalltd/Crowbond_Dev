using Crowbond.Modules.Warehouse.Domain.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.Warehouse.Infrastructure.Tasks;

internal sealed class TaskTypeConfiguration : IEntityTypeConfiguration<TaskType>
{
    public void Configure(EntityTypeBuilder<TaskType> builder)
    {
        builder.HasKey(t => t.Name);
        builder.Property(t => t.Name).IsRequired().HasMaxLength(100);
    }
}
