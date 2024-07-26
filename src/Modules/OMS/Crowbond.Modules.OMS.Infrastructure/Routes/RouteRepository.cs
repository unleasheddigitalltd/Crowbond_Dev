using Crowbond.Modules.OMS.Domain.Routes;
using Crowbond.Modules.OMS.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Crowbond.Modules.OMS.Infrastructure.Routes;

internal sealed class RouteRepository(OmsDbContext context) : IRouteRepository
{
    public async Task<Route?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Routes.SingleOrDefaultAsync(r => r.Id == id, cancellationToken);
    }

    public void Insert(Route route)
    {
        context.Routes.Add(route);
    }
}
