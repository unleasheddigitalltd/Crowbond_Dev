namespace Crowbond.Modules.OMS.Domain.RouteTripLogDatails;

public interface IRouteTripLogDatailRepository
{
    Task<RouteTripLogDatail?> GetAsync(Guid Id, CancellationToken cancellationToken = default);

    void Insert(RouteTripLogDatail routesTripLogDatail);
}
