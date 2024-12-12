using Crowbond.Modules.WMS.Domain.Sequences;

namespace Crowbond.Modules.WMS.Domain.Dispatches;

public interface IDispatchRepository
{
    Task<DispatchHeader?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    Task<DispatchHeader?> GetByRouteTripAsync(Guid routeTripId, CancellationToken cancellationToken = default);

    Task<Sequence?> GetSequenceAsync(CancellationToken cancellationToken = default);

    void Insert(DispatchHeader header);

    void AddLine(DispatchLine line);
}
