using Crowbond.Modules.OMS.Domain.RouteTrips;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.OMS.Infrastructure.RouteTrips;

internal sealed class RouteTripStatusHistoryConfiguration : IEntityTypeConfiguration<RouteTripStatusHistory>
{
    public void Configure(EntityTypeBuilder<RouteTripStatusHistory> builder)
    {
        builder.HasKey(r => r.Id);
    }
}
