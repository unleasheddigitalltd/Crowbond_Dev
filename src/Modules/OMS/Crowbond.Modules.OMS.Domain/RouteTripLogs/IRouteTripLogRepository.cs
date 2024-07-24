namespace Crowbond.Modules.OMS.Domain.RouteTripLogs;

public interface IRouteTripLogRepository
{
    Task<RouteTripLog> GetAsync(Guid Id, CancellationToken cancellationToken = default);

    void Insert(RouteTripLog routeTripLog);
}
