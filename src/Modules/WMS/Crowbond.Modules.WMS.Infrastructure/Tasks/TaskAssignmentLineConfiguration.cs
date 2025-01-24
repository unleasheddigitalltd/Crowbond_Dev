using Crowbond.Modules.WMS.Domain.Locations;
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

        builder.Property(t => t.Qty).HasPrecision(10, 2);

        builder.HasOne<Location>().WithMany().HasForeignKey(t => t.FromLocationId);
        builder.HasOne<Location>().WithMany().HasForeignKey(t => t.ToLocationId);
        builder.HasOne<Product>().WithMany().HasForeignKey(t => t.ProductId);

        builder.HasOne(al => al.Assignment)
               .WithMany(a => a.Lines)
               .HasForeignKey(al => al.TaskAssignmentId)
               .IsRequired();
    }
}
