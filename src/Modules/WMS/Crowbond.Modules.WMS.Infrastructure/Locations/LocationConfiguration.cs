using Crowbond.Modules.WMS.Domain.Locations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.WMS.Infrastructure.Locations;

internal sealed class LocationConfiguration : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.HasKey(l => l.Id);
        builder.Property(l => l.Name).IsRequired().HasMaxLength(100);
        builder.Property(l => l.LocationTypeName).IsRequired().HasMaxLength(100);

        builder.HasOne<LocationType>().WithMany().HasForeignKey(l => l.LocationTypeName);
        builder.HasOne<Location>().WithMany().HasForeignKey(l => l.ParentId);
    }
}
