using Crowbond.Modules.OMS.Domain.RouteTrips;
using Crowbond.Modules.OMS.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Crowbond.Modules.OMS.Infrastructure.RouteTrips;

internal sealed class RouteTripStatusHistoryRepository(OmsDbContext context) : IRouteTripStatusHistoryRepository
{
    public async Task<RouteTripStatusHistory?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.RouteTripStatusHistories.SingleOrDefaultAsync(r => r.Id == id, cancellationToken);
    }

    public void Insert(RouteTripStatusHistory routeTripStatusHistory)
    {
        context.RouteTripStatusHistories.Add(routeTripStatusHistory);
    }
}
