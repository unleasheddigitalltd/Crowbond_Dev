namespace Crowbond.Modules.OMS.Domain.RouteTrips;

public interface IRouteTripRepository
{
    Task<RouteTrip?> GetAsync(Guid id, CancellationToken cancellationToken = default);
    
    Task<RouteTrip?> GetByDateAndRouteAsync(DateOnly date, Guid routeId, CancellationToken cancellationToken = default);

    Task<RouteTrip[]?> GetByDateAsync(DateOnly date, CancellationToken cancellationToken = default);
    
    void Insert(RouteTrip routeTrip);
}
