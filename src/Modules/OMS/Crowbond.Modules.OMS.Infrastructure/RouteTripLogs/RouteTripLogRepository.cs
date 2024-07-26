using Crowbond.Modules.OMS.Domain.RouteTripLogs;
using Crowbond.Modules.OMS.Domain.RouteTrips;
using Crowbond.Modules.OMS.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Crowbond.Modules.OMS.Infrastructure.RouteTripLogs;

internal sealed class RouteTripLogRepository(OmsDbContext context) : IRouteTripLogRepository
{
    public async Task<RouteTripLog> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.RouteTripLogs.SingleOrDefaultAsync(l => l.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<RouteTripLog>> GetForRouteTripAsync(RouteTrip routeTrip, CancellationToken cancellationToken = default)
    {
        return await context.RouteTripLogs.Where(l => l.RouteTripId == routeTrip.Id).ToListAsync(cancellationToken);
    }

    public void Insert(RouteTripLog routeTripLog)
    {
        context.RouteTripLogs.Add(routeTripLog);
    }
}
