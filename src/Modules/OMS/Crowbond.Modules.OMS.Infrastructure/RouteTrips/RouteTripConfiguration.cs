using Crowbond.Modules.OMS.Domain.Routes;
using Crowbond.Modules.OMS.Domain.RouteTrips;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.OMS.Infrastructure.RouteTrips;

internal sealed class RouteTripConfiguration : IEntityTypeConfiguration<RouteTrip>
{
    public void Configure(EntityTypeBuilder<RouteTrip> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Comments).HasMaxLength(255);

        builder.HasOne<Route>().WithMany().HasForeignKey(r => r.RouteId).OnDelete(DeleteBehavior.NoAction);
    }
}
