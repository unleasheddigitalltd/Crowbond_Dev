namespace Crowbond.Modules.OMS.Domain.RouteTrips;

public interface IRouteTripStatusHistoryRepository
{
    Task<RouteTripStatusHistory?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    void Insert(RouteTripStatusHistory routeTripStatusHistory);
}
