using Crowbond.Modules.WMS.Domain.Sequences;

namespace Crowbond.Modules.WMS.Domain.Tasks;

public interface ITaskRepository
{
    Task<TaskHeader?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Sequence?> GetSequenceAsync(CancellationToken cancellationToken = default);

    void Insert(TaskHeader taskHeader);

    void AddAssignment(TaskAssignment assignment);
}
