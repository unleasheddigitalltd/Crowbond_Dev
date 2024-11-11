using Crowbond.Modules.OMS.Application.Vehicles.GetVehicles;
using Crowbond.Modules.OMS.Domain.Drivers;
using Crowbond.Modules.OMS.Domain.RouteTripLogs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.OMS.Infrastructure.RouteTripLogs;

internal sealed class RouteTripLogConfiguration : IEntityTypeConfiguration<RouteTripLog>
{
    public void Configure(EntityTypeBuilder<RouteTripLog> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne<Driver>().WithMany().HasForeignKey(x => x.DriverId).OnDelete(DeleteBehavior.NoAction);
    }
}
