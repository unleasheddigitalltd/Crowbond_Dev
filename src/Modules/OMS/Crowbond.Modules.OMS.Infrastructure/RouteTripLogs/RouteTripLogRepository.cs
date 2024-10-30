using Crowbond.Modules.OMS.Domain.RouteTripLogs;
using Crowbond.Modules.OMS.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Crowbond.Modules.OMS.Infrastructure.RouteTripLogs;

internal sealed class RouteTripLogRepository(OmsDbContext context) : IRouteTripLogRepository
{
    public async Task<RouteTripLog?> GetActiveByRouteTripIdAsync(Guid routeTripId, CancellationToken cancellationToken = default)
    {
        return await context.RouteTripLogs.SingleOrDefaultAsync(l => l.RouteTripId == routeTripId && l.LoggedOffTime == null, cancellationToken);
    }

    public async Task<RouteTripLog?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.RouteTripLogs.SingleOrDefaultAsync(l => l.Id == id, cancellationToken);
    }

    public void Insert(RouteTripLog routeTripLog)
    {
        context.RouteTripLogs.Add(routeTripLog);
    }
}
