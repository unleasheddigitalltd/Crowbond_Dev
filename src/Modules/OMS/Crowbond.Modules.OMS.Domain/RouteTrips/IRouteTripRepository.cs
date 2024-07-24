namespace Crowbond.Modules.OMS.Domain.RouteTrips;

public interface IRouteTripRepository
{
    Task<RouteTrip?> GetAsync(Guid Id, CancellationToken cancellationToken = default);

    void Insert(RouteTrip routeTrip);
}
