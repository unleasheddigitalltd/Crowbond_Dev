using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Domain.Products;

namespace Crowbond.Modules.WMS.Domain.Tasks;

public sealed class TaskAssignment : Entity
{
    private readonly List<TaskAssignmentLine> _lines = new();

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

    public IReadOnlyCollection<TaskAssignmentLine> Lines => _lines;

    internal static Result<TaskAssignment> Create(
        Guid assignedOperatorId,
        Guid createdBy,
        DateTime createdDate)
    {
        var taskAssignment = new TaskAssignment
        {
            Id = Guid.NewGuid(),
            AssignedOperatorId = assignedOperatorId,
            Status = TaskAssignmentStatus.Pending,
            CreatedBy = createdBy,
            CreatedDate = createdDate
        };

        return taskAssignment;
    }

    internal Result<TaskAssignmentLine> AddLine(Guid productId, decimal requestedQty)
    {
        if (Status != TaskAssignmentStatus.Pending)
        {
            return Result.Failure<TaskAssignmentLine>(TaskErrors.alreadyStarted);
        }

        Result<TaskAssignmentLine> result = 
            TaskAssignmentLine.Create(productId, requestedQty);

        if (result.IsFailure)
        {
            return Result.Failure<TaskAssignmentLine>(result.Error);
        }

        _lines.Add(result.Value);
        return result.Value;
    }

}
