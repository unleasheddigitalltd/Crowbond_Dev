using Crowbond.Modules.WMS.Domain.Locations;
using Crowbond.Modules.WMS.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Crowbond.Modules.WMS.Infrastructure.Locations;

internal sealed class LocationRepository(WmsDbContext context) : ILocationRepository
{
    public async Task<Location?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Locations.SingleOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Location>> GetChildrenAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Locations.Where(e => e.ParentId == id).ToListAsync(cancellationToken);
    }

    public void Insert(Location location)
    {
        context.Locations.Add(location);
    }

    public void Remove(Location location)
    {
        context.Locations.Remove(location);
    }
}
