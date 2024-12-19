namespace Crowbond.Modules.WMS.Domain.Locations;

public interface ILocationRepository
{
    Task<Location?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IEnumerable<Location>> GetChildrenAsync(Guid id, CancellationToken cancellationToken = default);

    void Insert(Location location);

    void Remove(Location location);
}
