using System.Data;
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

    internal Result<TaskAssignmentLine> AddLine(Guid productId, decimal requestedQty, Guid receiptLineId)
    {
        // check the Task assignment was not started.
        if (Status != TaskAssignmentStatus.Pending)
        {
            return Result.Failure<TaskAssignmentLine>(TaskErrors.AlreadyStarted);
        }

        // create a task assignment line.
        Result<TaskAssignmentLine> result = 
            TaskAssignmentLine.Create(productId, requestedQty, receiptLineId);

        if (result.IsFailure)
        {
            return Result.Failure<TaskAssignmentLine>(result.Error);
        }

        // add the created line to the lines.
        _lines.Add(result.Value);
        return result.Value;
    }

    internal Result Start(Guid modifiedBy, DateTime modificationDate)
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
        LastModifiedBy = modifiedBy;
        LastModifiedDate = modificationDate;

        return Result.Success();
    }

    internal Result Pause(Guid modifiedBy, DateTime modificationDate)
    {
        // check the task not already started.
        if (Status is not TaskAssignmentStatus.InProgress)
        {
            return Result.Failure<TaskAssignmentLine>(TaskErrors.AlreadyStarted);
        }

        // update the status.
        Status = TaskAssignmentStatus.Paused;
        LastModifiedBy = modifiedBy;
        LastModifiedDate = modificationDate;

        return Result.Success();
    }

    internal Result Unpause(Guid modifiedBy, DateTime modificationDate)
    {
        // check the task not already started.
        if (Status is not TaskAssignmentStatus.Paused)
        {
            return Result.Failure<TaskAssignmentLine>(TaskErrors.AlreadyInprogress);
        }

        // update the status.
        Status = TaskAssignmentStatus.InProgress;
        LastModifiedBy = modifiedBy;
        LastModifiedDate = modificationDate;

        return Result.Success();
    }

    internal Result Quit(Guid modifiedBy, DateTime modificationDate)
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
        LastModifiedBy = modifiedBy;
        LastModifiedDate = modificationDate;

        return Result.Success();
    }

    internal Result<TaskAssignmentLine> IncrementCompletedQty(Guid modifiedBy, DateTime modificationDate, Guid productId, decimal Quantity)
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

        LastModifiedBy = modifiedBy;
        LastModifiedDate = modificationDate;

        return Result.Success(line);
    }
}
