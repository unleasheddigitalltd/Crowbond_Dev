using Crowbond.Modules.WMS.Domain.Locations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.WMS.Infrastructure.Locations;

internal sealed class LocationTypeConfiguration : IEntityTypeConfiguration<LocationType>
{
    public void Configure(EntityTypeBuilder<LocationType> builder)
    {
        builder.HasKey(l => l.Name);
        builder.Property(l => l.Name).IsRequired().HasMaxLength(100);

        builder.HasData(
            LocationType.Site,
            LocationType.Area,
            LocationType.Location);
    }
}
