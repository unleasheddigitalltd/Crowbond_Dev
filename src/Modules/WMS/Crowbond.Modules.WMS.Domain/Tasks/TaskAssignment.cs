using System.Data;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Domain.Products;

namespace Crowbond.Modules.WMS.Domain.Tasks;

public sealed class TaskAssignment : Entity , IAuditable
{
    private readonly List<TaskAssignmentLine> _lines = new();

    private TaskAssignment()
    {      
    }

    public Guid Id { get; private set; }

    public Guid TaskHeaderId { get; private set; }

    public Guid AssignedOperatorId { get; private set; }

    public DateTime? StartDateTime { get; private set; }

    public DateTime? EndDateTime { get; private set; }

    public TaskAssignmentStatus Status { get; private set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedOnUtc { get; set; }

    public Guid? LastModifiedBy { get; set; }

    public DateTime? LastModifiedOnUtc { get; set; }

    public IReadOnlyCollection<TaskAssignmentLine> Lines => _lines;

    public TaskHeader Header { get; }

    internal static Result<TaskAssignment> Create(
        Guid assignedOperatorId)
    {
        var taskAssignment = new TaskAssignment
        {
            Id = Guid.NewGuid(),
            AssignedOperatorId = assignedOperatorId,
            Status = TaskAssignmentStatus.Pending
        };

        return taskAssignment;
    }

    internal Result<TaskAssignmentLine> AddLine(
        Guid? receiptLineId, 
        Guid? dispatchLineId,
        Guid fromLocationId,
        Guid toLocationId,
        Guid productId,
        decimal qty)
    {
        // check the Task assignment was not started.
        if (Status != TaskAssignmentStatus.InProgress)
        {
            return Result.Failure<TaskAssignmentLine>(TaskErrors.NotInProgress);
        }

        // create a task assignment line.
        Result<TaskAssignmentLine> result = 
            TaskAssignmentLine.Create(
                receiptLineId,
                dispatchLineId,
                fromLocationId, 
                toLocationId,
                productId,
                qty);

        if (result.IsFailure)
        {
            return Result.Failure<TaskAssignmentLine>(result.Error);
        }

        // add the created line to the lines.
        _lines.Add(result.Value);
        return result.Value;
    }

    internal Result Start(DateTime utcNow)
    {
        // check the task not already started.
        if (Status is not TaskAssignmentStatus.Pending and not TaskAssignmentStatus.Paused)
        {
            return Result.Failure<TaskAssignmentLine>(TaskErrors.AlreadyStarted);
        }

        StartDateTime = utcNow;

        // update the status.
        Status = TaskAssignmentStatus.InProgress;

        return Result.Success();
    }

    internal Result Pause()
    {
        // check the task not already started.
        if (Status is not TaskAssignmentStatus.InProgress)
        {
            return Result.Failure<TaskAssignmentLine>(TaskErrors.AlreadyStarted);
        }

        // update the status.
        Status = TaskAssignmentStatus.Paused;

        return Result.Success();
    }

    internal Result Unpause()
    {
        // check the task not already started.
        if (Status is not TaskAssignmentStatus.Paused)
        {
            return Result.Failure<TaskAssignmentLine>(TaskErrors.AlreadyInprogress);
        }

        // update the status.
        Status = TaskAssignmentStatus.InProgress;

        return Result.Success();
    }

    internal Result Quit(DateTime utcNow)
    {
        // check the task not already started.
        if (Status is not TaskAssignmentStatus.InProgress and not TaskAssignmentStatus.Paused)
        {
            return Result.Failure<TaskAssignmentLine>(TaskErrors.NotInProgress);
        }

        EndDateTime = utcNow;

        // update the status.
        Status = TaskAssignmentStatus.Quit;

        return Result.Success();
    }

    internal Result Complete(DateTime utcNow)
    {
        // check the task not already started.
        if (Status is not TaskAssignmentStatus.InProgress and not TaskAssignmentStatus.Paused)
        {
            return Result.Failure<TaskAssignmentLine>(TaskErrors.NotInProgress);
        }

        EndDateTime = utcNow;

        // update the status.
        Status = TaskAssignmentStatus.Completed;

        return Result.Success();
    }    
}
