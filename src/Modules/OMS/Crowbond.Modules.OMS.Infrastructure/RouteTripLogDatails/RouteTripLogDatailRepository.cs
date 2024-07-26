using Crowbond.Modules.OMS.Domain.RouteTripLogDatails;
using Crowbond.Modules.OMS.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Crowbond.Modules.OMS.Infrastructure.RouteTripLogDatails;

internal sealed class RouteTripLogDatailRepository(OmsDbContext context) : IRouteTripLogDatailRepository
{
    public async Task<RouteTripLogDatail?> GetAsync(Guid Id, CancellationToken cancellationToken = default)
    {
        return await context.RouteTripLogDatails.SingleOrDefaultAsync(r => r.Id == Id, cancellationToken);
    }

    public void Insert(RouteTripLogDatail routesTripLogDatail)
    {
        context.RouteTripLogDatails.Add(routesTripLogDatail);
    }
}
