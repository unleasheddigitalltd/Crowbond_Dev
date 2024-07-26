namespace Crowbond.Modules.OMS.Domain.Routes;

public interface IRouteRepository
{
    Task<Route?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    void Insert(Route route);
}
