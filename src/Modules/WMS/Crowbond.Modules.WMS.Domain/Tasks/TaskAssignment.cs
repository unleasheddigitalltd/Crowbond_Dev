using Crowbond.Common.Domain;

namespace Crowbond.Modules.WMS.Domain.Tasks;

public sealed class TaskAssignment : Entity
{
    private TaskAssignment()
    {        
    }

    public Guid Id { get; private set; }

    public Guid TaskHeaderId { get; private set; }

    public Guid AssignedOperatorId { get; private set; }

    public TaskAssignmentStatus Status { get; private set; }

    public Guid CreatedBy { get; private set; }

    public DateTime CreatedDate { get; private set; }

    public Guid? LastModifiedBy { get; private set; }

    public DateTime? LastModifiedDate { get; private set; }

    public TaskAssignment Create(
        Guid taskHeaderId,
        Guid assignedOperatorId,
        TaskAssignmentStatus status,
        Guid createdBy,
        DateTime createdDate)
    {
        var taskAssignment = new TaskAssignment
        {
            Id = Guid.NewGuid(),
            TaskHeaderId = taskHeaderId,
            AssignedOperatorId = assignedOperatorId,
            Status = status,
            CreatedBy = createdBy,
            CreatedDate = createdDate
        };

        return taskAssignment;
    }
}
