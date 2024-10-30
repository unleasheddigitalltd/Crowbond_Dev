using Crowbond.Modules.OMS.Domain.RouteTrips;

namespace Crowbond.Modules.OMS.Domain.RouteTripLogs;

public interface IRouteTripLogRepository
{
    Task<RouteTripLog?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    Task<RouteTripLog?> GetActiveByRouteTripIdAsync(Guid routeTripId, CancellationToken cancellationToken = default);

    void Insert(RouteTripLog routeTripLog);
}
