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

    internal Result<TaskAssignmentLine> AddLine(Guid productId, decimal requestedQty)
    {
        // check the Task assignment was not started.
        if (Status != TaskAssignmentStatus.Pending)
        {
            return Result.Failure<TaskAssignmentLine>(TaskErrors.AlreadyStarted);
        }

        // create a task assignment line.
        Result<TaskAssignmentLine> result = 
            TaskAssignmentLine.Create(productId, requestedQty);

        if (result.IsFailure)
        {
            return Result.Failure<TaskAssignmentLine>(result.Error);
        }

        // add the created line to the lines.
        _lines.Add(result.Value);
        return result.Value;
    }

    internal Result Start(DateTime modificationDate)
    {
        // check the task not already started.
        if (Status is not TaskAssignmentStatus.Pending and not TaskAssignmentStatus.Paused)
        {
            return Result.Failure<TaskAssignmentLine>(TaskErrors.AlreadyStarted);
        }

        // start all the lines.
        foreach (TaskAssignmentLine line in _lines)
        {
            Result result = line.Start(modificationDate);
            if (result.IsFailure)
            {
                return result;
            }
        }

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

    internal Result Quit(DateTime modificationDate)
    {
        // check the task not already started.
        if (Status is not TaskAssignmentStatus.InProgress and not TaskAssignmentStatus.Paused)
        {
            return Result.Failure<TaskAssignmentLine>(TaskErrors.NotInProgress);
        }

        // close all the incomplete lines.
        IEnumerable<TaskAssignmentLine> notCompleteLines = 
            _lines.Where(l => l.Status is not TaskAssignmentLineStatus.Completed);
        
        foreach (TaskAssignmentLine line in notCompleteLines)
        {
            Result result = line.Close(modificationDate);
            if (result.IsFailure)
            {
                return result;
            }
        }

        // update the status.
        Status = TaskAssignmentStatus.Quit;

        return Result.Success();
    }

    internal Result Complete(DateTime modificationDate)
    {
        // check the task not already started.
        if (Status is not TaskAssignmentStatus.InProgress and not TaskAssignmentStatus.Paused)
        {
            return Result.Failure<TaskAssignmentLine>(TaskErrors.NotInProgress);
        }

        // complete all the incomplete lines.
        IEnumerable<TaskAssignmentLine> notCompleteLines = 
            _lines.Where(l => l.Status is not TaskAssignmentLineStatus.Completed);
        
        foreach (TaskAssignmentLine line in notCompleteLines)
        {
            Result result = line.Complete(modificationDate);
            if (result.IsFailure)
            {
                return result;
            }
        }

        // update the status.
        Status = TaskAssignmentStatus.Completed;

        return Result.Success();
    }

    internal Result<TaskAssignmentLine> IncrementCompletedQty(DateTime modificationDate, Guid productId, decimal Quantity)
    {
        // select the specific line with this product.
        TaskAssignmentLine? line = _lines.Find(l => l.ProductId == productId);

        if (line is null)
        {
            return Result.Failure<TaskAssignmentLine>(TaskErrors.LineForProductNotFound(productId));
        }

        // increament the complete quantity.
        Result result = line.IncrementCompletedQty(modificationDate, Quantity);

        if (result.IsFailure)
        {
            return Result.Failure<TaskAssignmentLine>(result.Error);
        }

        // change status to completed all lines are completed.
        if (_lines.TrueForAll(l => l.Status is TaskAssignmentLineStatus.Completed))
        {
            Status = TaskAssignmentStatus.Completed;
        }

        return Result.Success(line);
    }
}
