using Crowbond.Common.Application.Clock;
using Crowbond.Common.Domain;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Crowbond.Modules.WMS.Domain.Tasks;

public sealed class TaskHeader : Entity
{
    private readonly List<TaskAssignment> _assignments = new();

    private TaskHeader()
    {
    }

    public Guid Id { get; private set; }

    public string TaskNo { get; private set; }

    public Guid EntityId { get; private set; }

    public TaskType TaskType { get; private set; }

    public IReadOnlyCollection<TaskAssignment> Assignments => _assignments;

    public static Result<TaskHeader> Create(
        string taskNo,
        Guid entityId,
        TaskType taskType)
    {
        var taskHeader = new TaskHeader
        {
            Id = Guid.NewGuid(),
            TaskNo = taskNo,
            EntityId = entityId,
            TaskType = taskType
        };

        return taskHeader;
    }

    public Result<TaskAssignment> AddAssignment(
        Guid warehouseOperatorId,
        Guid createdBy,
        DateTime createdDate)
    {
        if (_assignments.Any(a => 
            a.Status == TaskAssignmentStatus.Pending ||
            a.Status == TaskAssignmentStatus.InProgress))
        {
            return Result.Failure<TaskAssignment>(TaskErrors.HasInProggressAssignment(Id));
        }

        Result<TaskAssignment> result = TaskAssignment.Create(
           warehouseOperatorId,
           createdBy,
           createdDate);

        if (result.IsSuccess)
        {
            _assignments.Add(result.Value);
        }

        return result;
    }

    public Result<TaskAssignmentLine> AddAssignmentLine(
        Guid productId,
        decimal requestedQty)
    {
        TaskAssignment? assignment = _assignments.Find(a =>
            a.Status == TaskAssignmentStatus.Pending);

        if (assignment is null)
        {
            return Result.Failure<TaskAssignmentLine>(TaskErrors.HasNoPendingAssignment(Id));
        }

        Result<TaskAssignmentLine> result = assignment.AddLine(productId, requestedQty);

        return result;
    }

    public Result Start(Guid modifiedBy, DateTime modifiedDate)
    {
        TaskAssignment? assignment = _assignments.Find(a => 
            a.Status == TaskAssignmentStatus.Pending ||
            a.Status == TaskAssignmentStatus.Paused);

        if (assignment is null)
        {
            return Result.Failure<TaskAssignmentLine>(TaskErrors.HasNoPendingAssignment(Id));
        }

        return assignment.Start(modifiedBy, modifiedDate);
    }
}
