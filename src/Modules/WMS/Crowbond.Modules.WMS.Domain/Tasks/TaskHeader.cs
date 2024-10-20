﻿using Crowbond.Common.Domain;

namespace Crowbond.Modules.WMS.Domain.Tasks;

public sealed class TaskHeader : Entity, IChangeDetectable
{
    private readonly List<TaskAssignment> _assignments = new();

    private TaskHeader()
    {
    }

    public Guid Id { get; private set; }

    public string TaskNo { get; private set; }

    public Guid? ReceiptId { get; private set; }

    public Guid? DispatchId { get; private set; }

    public TaskType TaskType { get; private set; }

    public TaskHeaderStatus Status { get; private set; }

    public IReadOnlyCollection<TaskAssignment> Assignments => _assignments;

    public static Result<TaskHeader> Create(
        string taskNo,
        Guid? receiptId,
        Guid? dispatchId,
        TaskType taskType)
    {
        var taskHeader = new TaskHeader
        {
            Id = Guid.NewGuid(),
            TaskNo = taskNo,
            ReceiptId = receiptId,
            DispatchId = dispatchId,
            TaskType = taskType,
            Status = TaskHeaderStatus.NotAssigned,
        };

        return taskHeader;
    }

    public Result<TaskAssignment> AddAssignmentWithLines(
        Guid warehouseOperatorId,
        List<(Guid productId, decimal requestedQty)> productLines)
    {
        switch (Status)
        {
            case TaskHeaderStatus.NotAssigned:

                var productCompeletedQties = _assignments
                    .SelectMany(a => a.Lines)       // Flatten to get all lines
                    .GroupBy(line => line.ProductId) // Group by ProductId
                    .Select(group => new
                    {
                        productId = group.Key, // The key is the ProductId
                        totalCompleteQty = group.Sum(line => line.CompletedQty) // Sum of CompleteQty for each product
                    })
                    .ToList();

                // Create a new task assignment
                Result<TaskAssignment> createResult = TaskAssignment.Create(warehouseOperatorId);

                if (createResult.IsFailure)
                {
                    return createResult;
                }

                TaskAssignment newAssignment = createResult.Value;

                // Attempt to add lines for each product
                foreach ((Guid productId, decimal requestedQty) in productLines)
                {
                    // consider the compelete quantity of previous task assignments.
                    decimal newRequestedQty = requestedQty - (productCompeletedQties.Find(p => p.productId == productId)?.totalCompleteQty ?? 0);

                    if (newRequestedQty > 0)
                    {
                        Result<TaskAssignmentLine> addLineResult = newAssignment.AddLine(productId, newRequestedQty);

                        if (!addLineResult.IsSuccess)
                        {
                            // If adding a line fails, return failure result
                            return Result.Failure<TaskAssignment>(addLineResult.Error);
                        }
                    }
                }

                // Add the new assignment to the list
                _assignments.Add(newAssignment);
                Status = TaskHeaderStatus.Assigned;

                return createResult;

            case TaskHeaderStatus.Assigned:
                return Result.Failure<TaskAssignment>(TaskErrors.AlreadyAssigned);

            case TaskHeaderStatus.InProgress:
                return Result.Failure<TaskAssignment>(TaskErrors.IsInProgress);

            case TaskHeaderStatus.Completed:
                return Result.Failure<TaskAssignment>(TaskErrors.IsCompleted);

            case TaskHeaderStatus.Canceled:
                return Result.Failure<TaskAssignment>(TaskErrors.IsCanceled);

            default:
                // Handle unexpected status values
                return Result.Failure<TaskAssignment>(TaskErrors.UnknownStatus(Status));
        }
    }

    public Result Start(DateTime modificationDate)
    {
        switch (Status)
        {
            case TaskHeaderStatus.NotAssigned:
                return Result.Failure<TaskAssignmentLine>(TaskErrors.NotAssigned);

            case TaskHeaderStatus.Assigned:
                // Attempt to find an assignment that is either Pending or Paused
                TaskAssignment? assignment = _assignments
                    .Find(a => a.Status is TaskAssignmentStatus.Pending or TaskAssignmentStatus.Paused);

                if (assignment is null)
                {
                    return Result.Failure<TaskAssignmentLine>(TaskErrors.HasNoPendingAssignment(Id));
                }

                // start the found assignment
                Result result = assignment.Start(modificationDate);
                if (result.IsFailure)
                {
                    return result;
                }

                // Transition task status to InProgress
                Status = TaskHeaderStatus.InProgress;
                return Result.Success();

            case TaskHeaderStatus.InProgress:
                return Result.Failure<TaskAssignmentLine>(TaskErrors.AlreadyStarted);

            case TaskHeaderStatus.Completed:
                return Result.Failure<TaskAssignmentLine>(TaskErrors.IsCompleted);

            case TaskHeaderStatus.Canceled:
                return Result.Failure<TaskAssignmentLine>(TaskErrors.IsCanceled);

            default:
                // Unexpected status - consider reviewing status transitions
                return Result.Failure<TaskAssignmentLine>(TaskErrors.UnknownStatus(Status));
        }
    }

    public Result Pause()
    {
        if (Status is not TaskHeaderStatus.InProgress)
        {
            return Result.Failure(TaskErrors.NotInProgress);
        }

        // Attempt to find an assignment that is either Pending or Paused
        TaskAssignment? assignment = _assignments
            .Find(a => a.Status is TaskAssignmentStatus.InProgress);

        if (assignment is null)
        {
            return Result.Failure<TaskAssignmentLine>(TaskErrors.HasNoInprogressAssignmet(Id));
        }

        // pause the found assignment
        Result result = assignment.Pause();
        if (result.IsFailure)
        {
            return result;
        }

        return Result.Success();
    }

    public Result Unpause()
    {
        if (Status is not TaskHeaderStatus.InProgress)
        {
            return Result.Failure(TaskErrors.NotInProgress);
        }

        // Attempt to find an assignment that is either Pending or Paused
        TaskAssignment? assignment = _assignments
            .Find(a => a.Status is TaskAssignmentStatus.Paused);

        if (assignment is null)
        {
            return Result.Failure<TaskAssignmentLine>(TaskErrors.HasNoPausedAssignmet(Id));
        }

        // pause the found assignment
        Result result = assignment.Unpause();
        if (result.IsFailure)
        {
            return result;
        }

        return Result.Success();
    }

    public Result Quit(DateTime modificationDate)
    {
        if (Status is not TaskHeaderStatus.InProgress)
        {
            return Result.Failure(TaskErrors.NotInProgress);
        }

        // Attempt to find an assignment that is either Pending or Paused
        TaskAssignment? assignment = _assignments
            .Find(a => a.Status is TaskAssignmentStatus.InProgress or TaskAssignmentStatus.Paused);

        if (assignment is null)
        {
            return Result.Failure<TaskAssignmentLine>(TaskErrors.HasNoInprogressAssignmet(Id));
        }

        // pause the found assignment
        Result result = assignment.Quit(modificationDate);
        if (result.IsFailure)
        {
            return result;
        }

        Status = TaskHeaderStatus.NotAssigned;
        return Result.Success();
    }

    public Result Complete(DateTime modificationDate)
    {
        if (Status is not TaskHeaderStatus.InProgress)
        {
            return Result.Failure(TaskErrors.NotInProgress);
        }

        // Attempt to find an assignment that is either Pending or Paused
        TaskAssignment? assignment = _assignments
            .Find(a => a.Status is TaskAssignmentStatus.InProgress or TaskAssignmentStatus.Paused);

        if (assignment is null)
        {
            return Result.Failure<TaskAssignmentLine>(TaskErrors.HasNoInprogressAssignmet(Id));
        }

        // pause the found assignment
        Result result = assignment.Complete(modificationDate);
        if (result.IsFailure)
        {
            return result;
        }

        Status = TaskHeaderStatus.Completed;
        return Result.Success();
    }

    public Result<TaskAssignmentLine> IncrementCompletedQty(DateTime modificationDate, Guid productId, decimal Quantity)
    {
        // Find the single assignment that is currently in progress
        TaskAssignment taskAssignment = _assignments.SingleOrDefault(a => a.Status is TaskAssignmentStatus.InProgress);

        if (taskAssignment is null)
        {
            return Result.Failure<TaskAssignmentLine>(TaskErrors.HasNoInprogressAssignmet(Id));
        }

        // Attempt to increase the assignment line with the specified parameters
        Result<TaskAssignmentLine> result = taskAssignment.IncrementCompletedQty(modificationDate, productId, Quantity);

        if (_assignments.Any(l => l.Status is TaskAssignmentStatus.Completed))
        {
            Status = TaskHeaderStatus.Completed;
        }

        return result;
    }

    public bool IsAssignedTo(Guid operatorId)
    {
        TaskAssignment? activeAssignment = _assignments.Find(a => 
            a.Status is not TaskAssignmentStatus.Quit or TaskAssignmentStatus.Completed);

        return activeAssignment?.AssignedOperatorId == operatorId;
    }

}
