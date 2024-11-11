using Crowbond.Modules.OMS.Domain.RouteTripLogs;
using Crowbond.Modules.OMS.Domain.RouteTrips;
using Crowbond.Modules.OMS.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Crowbond.Modules.OMS.Infrastructure.RouteTripLogs;

internal sealed class RouteTripLogRepository(OmsDbContext context) : IRouteTripLogRepository
{
    public async Task<RouteTripLog?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.RouteTripLogs.SingleOrDefaultAsync(l => l.Id == id, cancellationToken);
    }

    public async Task<RouteTripLog?> GetActiveByDateAndDriverExcludingRouteTrip(DateOnly currentDate, Guid routeTripId, Guid driverId, CancellationToken cancellationToken = default)
    {
        return await context.RouteTripLogs.FirstOrDefaultAsync(l =>
            DateOnly.FromDateTime(l.LoggedOnTime) == currentDate &&
            l.DriverId == driverId &&
            l.RouteTripId != routeTripId &&
            l.LoggedOffTime == null,
            cancellationToken);
    }

    public async Task<RouteTripLog?> GetActiveByDateAndRouteTripExcludingDriver(DateOnly currentDate, Guid routeTripId, Guid driverId, CancellationToken cancellationToken = default)
    {
        return await context.RouteTripLogs.FirstOrDefaultAsync(l =>
            DateOnly.FromDateTime(l.LoggedOnTime) == currentDate &&
            l.DriverId != driverId &&
            l.RouteTripId == routeTripId &&
            l.LoggedOffTime == null,
            cancellationToken);
    }

    public async Task<RouteTripLog?> GetActiveByDateAndDriverAndRouteTrip(DateOnly currentDate, Guid routeTripId, Guid driverId, CancellationToken cancellationToken = default)
    {
        return await context.RouteTripLogs.FirstOrDefaultAsync(l =>
            DateOnly.FromDateTime(l.LoggedOnTime) == currentDate &&
            l.DriverId == driverId &&
            l.RouteTripId == routeTripId &&
            l.LoggedOffTime == null,
            cancellationToken);
    }

    public async Task<RouteTripLog?> GetActiveByDateAndDriver(DateOnly currentDate, Guid driverId, CancellationToken cancellationToken = default)
    {
        return await context.RouteTripLogs.FirstOrDefaultAsync(l =>
            DateOnly.FromDateTime(l.LoggedOnTime) == currentDate &&
            l.DriverId == driverId &&
            l.LoggedOffTime == null,
            cancellationToken);
    }

    public void Insert(RouteTripLog routeTripLog)
    {
        context.RouteTripLogs.Add(routeTripLog);
    }
}
