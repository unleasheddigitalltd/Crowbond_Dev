using Crowbond.Modules.WMS.Domain.Dispatches;
using Crowbond.Modules.WMS.Domain.Sequences;
using Crowbond.Modules.WMS.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Crowbond.Modules.WMS.Infrastructure.Dispatches;

internal sealed class DispatchRepository(WmsDbContext context) : IDispatchRepository
{
    public async Task<DispatchHeader?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.DispatchHeaders.Include(d => d.Lines).SingleOrDefaultAsync(d => d.Id == id, cancellationToken);
    }

    public async Task<DispatchHeader?> GetByRouteTripAsync(Guid routeTripId, CancellationToken cancellationToken = default)
    {
        return await context.DispatchHeaders.Include(d => d.Lines).SingleOrDefaultAsync(d => d.RouteTripId == routeTripId, cancellationToken);
    }

    public void Insert(DispatchHeader header)
    {
        context.DispatchHeaders.Add(header);
    }

    public void AddLine(DispatchLine line)
    {
        context.DispatchLines.Add(line);
    }

    public async Task<Sequence?> GetSequenceAsync(CancellationToken cancellationToken = default)
    {
        return await context.Sequences.SingleOrDefaultAsync(s => s.Context == SequenceContext.Dispatch, cancellationToken);
    }
}
