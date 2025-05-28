using Crowbond.Modules.WMS.Domain.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.WMS.Infrastructure.Tasks;

internal sealed class TaskLineDispatchMappingConfiguration : IEntityTypeConfiguration<TaskLineDispatchMapping>
{
    public void Configure(EntityTypeBuilder<TaskLineDispatchMapping> builder)
    {
        builder.HasKey(m => m.Id);

        builder.Property(m => m.Id)
            .ValueGeneratedNever();

        builder.Property(m => m.TaskLineId)
            .IsRequired();

        builder.Property(m => m.DispatchLineId)
            .IsRequired();

        builder.Property(m => m.AllocatedQty)
            .HasPrecision(10, 2)
            .IsRequired();

        builder.Property(m => m.CompletedQty)
            .HasPrecision(10, 2)
            .IsRequired();

        builder.HasOne(m => m.TaskLine)
            .WithMany(t => t.DispatchMappings)
            .HasForeignKey(m => m.TaskLineId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(m => m.DispatchLineId)
            .IsUnique();
    }
}