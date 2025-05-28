using Crowbond.Modules.WMS.Domain.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.WMS.Infrastructure.Tasks;

internal sealed class TaskLineConfiguration : IEntityTypeConfiguration<TaskLine>
{
    public void Configure(EntityTypeBuilder<TaskLine> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .ValueGeneratedNever();

        builder.Property(t => t.TaskHeaderId)
            .IsRequired();

        builder.Property(t => t.FromLocationId)
            .IsRequired();

        builder.Property(t => t.ToLocationId)
            .IsRequired();

        builder.Property(t => t.ProductId)
            .IsRequired();

        builder.Property(t => t.TotalQty)
            .HasPrecision(10, 2)
            .IsRequired();

        builder.Property(t => t.CompletedQty)
            .HasPrecision(10, 2)
            .IsRequired();

        builder.HasOne(t => t.TaskHeader)
            .WithMany(th => th.Lines)
            .HasForeignKey(t => t.TaskHeaderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(t => t.DispatchMappings)
            .WithOne(m => m.TaskLine)
            .HasForeignKey(m => m.TaskLineId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(t => t.DispatchMappings)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}