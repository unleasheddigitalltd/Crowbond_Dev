using Crowbond.Common.Domain;

namespace Crowbond.Modules.WMS.Domain.Tasks;

public sealed class TaskAssignmentStatusHistory : Entity
{
    private TaskAssignmentStatusHistory()
    {        
    }

    public Guid Id { get; private set; }

    public Guid TaskAssignmentId { get; private set; }

    public TaskAssignmentStatus Status { get; private set; }

    public DateTime ChangedAt { get; private set; }

    public Guid ChangedBy { get; private set; }

    public static TaskAssignmentStatusHistory Create(
        Guid taskAssignmentId,
        TaskAssignmentStatus status,
        DateTime changedAt,
        Guid changedBy)
    {
        var taskAssignmentStatusHistory = new TaskAssignmentStatusHistory
        {
            Id = Guid.NewGuid(),
            TaskAssignmentId = taskAssignmentId,
            Status = status,
            ChangedAt = changedAt,
            ChangedBy = changedBy
        };

        return taskAssignmentStatusHistory;
    }
}
