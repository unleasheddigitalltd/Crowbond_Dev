using Crowbond.Modules.OMS.Domain.RouteTripLogs;
using Crowbond.Modules.OMS.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Crowbond.Modules.OMS.Infrastructure.RouteTripLogs;

internal sealed class RouteTripLogRepository(OmsDbContext context) : IRouteTripLogRepository
{
    public async Task<RouteTripLog> GetAsync(Guid Id, CancellationToken cancellationToken = default)
    {
        return await context.RouteTripLogs.SingleOrDefaultAsync(l => l.Id == Id, cancellationToken);
    }

    public void Insert(RouteTripLog routeTripLog)
    {
        context.RouteTripLogs.Add(routeTripLog);
    }
}
