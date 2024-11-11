namespace Crowbond.Modules.OMS.Domain.RouteTripLogs;

public interface IRouteTripLogRepository
{
    Task<RouteTripLog?> GetAsync(Guid id, CancellationToken cancellationToken = default);
    
    Task<RouteTripLog?> GetActiveByDateAndRouteTripExcludingDriver(DateOnly currentDate, Guid routeTripId, Guid driverId, CancellationToken cancellationToken = default);
    
    Task<RouteTripLog?> GetActiveByDateAndDriverExcludingRouteTrip(DateOnly currentDate, Guid routeTripId, Guid driverId, CancellationToken cancellationToken = default);
    
    Task<RouteTripLog?> GetActiveByDateAndDriverAndRouteTrip(DateOnly currentDate, Guid routeTripId, Guid driverId, CancellationToken cancellationToken = default);
    
    Task<RouteTripLog?> GetActiveByDateAndDriver(DateOnly currentDate, Guid driverId, CancellationToken cancellationToken = default);


    void Insert(RouteTripLog routeTripLog);
}
