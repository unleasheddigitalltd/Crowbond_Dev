using Crowbond.Modules.OMS.Domain.RouteTrips;
using Crowbond.Modules.OMS.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Crowbond.Modules.OMS.Infrastructure.RouteTrips;

internal sealed class RouteTripRepository(OmsDbContext context) : IRouteTripRepository
{
    public async Task<RouteTrip?> GetAsync(Guid id, CancellationToken cancellationToken = default) => 
        await context.RouteTrips.SingleOrDefaultAsync(r => r.Id == id, cancellationToken);

    public async Task<RouteTrip?> GetByDateAndRouteAsync(DateOnly date, Guid routeId, CancellationToken cancellationToken = default) =>
        await context.RouteTrips.SingleOrDefaultAsync(r => r.Date == date && r.RouteId == routeId,
            cancellationToken);

    public async Task<RouteTrip[]?> GetByDateAsync(DateOnly date, CancellationToken cancellationToken = default)
    {
        return await context.RouteTrips.Where(r => r.Date == date).ToArrayAsync(cancellationToken);
    }

    public void Insert(RouteTrip routeTrip) => 
        context.RouteTrips.Add(routeTrip);
}
