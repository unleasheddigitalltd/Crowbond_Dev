using Crowbond.Modules.CRM.Domain.Routes;
using Crowbond.Modules.CRM.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Crowbond.Modules.CRM.Infrastructure.Routes;

internal sealed class RouteRepository(CrmDbContext context) : IRouteRepository
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
