using Crowbond.Modules.WMS.Domain.Locations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.WMS.Infrastructure.Locations;

internal sealed class LocationConfiguration : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.HasKey(l => l.Id);

        builder.Property(l => l.Name).HasMaxLength(100);

        builder.Property(l => l.ScanCode).HasMaxLength(20);

        builder.HasOne<Location>().WithMany().HasForeignKey(l => l.ParentId);
    }
}
