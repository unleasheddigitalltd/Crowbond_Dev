using Crowbond.Modules.WMS.Domain.Sequences;

namespace Crowbond.Modules.WMS.Domain.Tasks;

public interface ITaskRepository
{
    Task<TaskHeader?> GetAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<TaskHeader>> GetByDispatchHeader(Guid dispatchHeaderId, CancellationToken cancellationToken = default);

    Task<TaskAssignmentLine?> GetAssignmentLineAsync(Guid id, CancellationToken cancellationToken= default);
        
    Task<TaskLine?> GetTaskLineAsync(Guid id, CancellationToken cancellationToken = default);
    
    Task<TaskHeader?> GetByRouteLocationAndTypeAsync(
        Guid routeTripId, 
        Guid locationId, 
        TaskType taskType, 
        CancellationToken cancellationToken = default);

    Task<Sequence?> GetSequenceAsync(CancellationToken cancellationToken = default);

    void Insert(TaskHeader taskHeader);

    void AddAssignment(TaskAssignment assignment);
    
    void AddAssignmentLine(TaskAssignmentLine assignmentLine);
    
    void AddTaskLine(TaskLine taskLine);
    
    void AddTaskLineDispatchMapping(TaskLineDispatchMapping mapping);
}
