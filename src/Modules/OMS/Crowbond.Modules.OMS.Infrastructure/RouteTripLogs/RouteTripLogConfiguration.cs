using Crowbond.Modules.OMS.Domain.Drivers;
using Crowbond.Modules.OMS.Domain.RouteTripLogs;
using Crowbond.Modules.OMS.Domain.RouteTrips;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.OMS.Infrastructure.RouteTripLogs;

internal sealed class RouteTripLogConfiguration : IEntityTypeConfiguration<RouteTripLog>
{
    public void Configure(EntityTypeBuilder<RouteTripLog> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Temperature).IsRequired(false).HasPrecision(2, 2);
        builder.Property(x => x.VehicleRegn).IsRequired().HasMaxLength(10);

        builder.HasOne<RouteTrip>().WithMany().HasForeignKey(x => x.RouteTripId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne<Driver>().WithMany().HasForeignKey(x => x.DriverId).OnDelete(DeleteBehavior.NoAction);
    }
}
