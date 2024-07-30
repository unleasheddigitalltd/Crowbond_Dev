using Crowbond.Modules.OMS.Domain.RouteTrips;
using Crowbond.Modules.OMS.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Crowbond.Modules.OMS.Infrastructure.RouteTrips;

internal sealed class RouteTripRepository(OmsDbContext context) : IRouteTripRepository
{
    public async Task<RouteTrip?> GetAsync(Guid Id, CancellationToken cancellationToken = default)
    {
        return await context.RouteTrips.SingleOrDefaultAsync(r => r.Id == Id, cancellationToken);
    }

    public void Insert(RouteTrip routeTrip)
    {
        context.RouteTrips.Add(routeTrip);
    }
}
