using Crowbond.Common.Domain;

namespace Crowbond.Modules.WMS.Domain.Tasks;

public class TaskHeader : Entity, IChangeDetectable
{
    private readonly List<TaskAssignment> _assignments = new();
    private readonly List<TaskLine> _lines = new();

    private TaskHeader()
    {
    }

    public Guid Id { get; private set; }

    public string TaskNo { get; private set; }

    public Guid? ReceiptId { get; private set; }

    public Guid? DispatchId { get; private set; }
    
    public Guid? LocationId { get; private set; }
    
    public Guid? RouteTripId { get; private set; }
    
    public DateOnly? ScheduledDeliveryDate { get; private set; }

    public TaskType TaskType { get; private set; }

    public TaskHeaderStatus Status { get; private set; }

    public IReadOnlyCollection<TaskAssignment> Assignments => _assignments;
    
    public IReadOnlyCollection<TaskLine> Lines => _lines;

    public static Result<TaskHeader> Create(
        string taskNo,
        Guid? receiptId,
        Guid? dispatchId,
        Guid? locationId,
        Guid? routeTripId,
        DateOnly? scheduledDeliveryDate,
        TaskType taskType)
    {
        var taskHeader = new TaskHeader
        {
            Id = Guid.NewGuid(),
            TaskNo = taskNo,
            ReceiptId = receiptId,
            DispatchId = dispatchId,
            LocationId = locationId,
            RouteTripId = routeTripId,
            ScheduledDeliveryDate = scheduledDeliveryDate,
            TaskType = taskType,
            Status = TaskHeaderStatus.NotAssigned,
        };

        return taskHeader;
    }

    public Result<TaskAssignment> AddAssignment(Guid warehouseOperatorId)
    {
        if (Status != TaskHeaderStatus.NotAssigned || _assignments.Any(a => 
        a.Status == TaskAssignmentStatus.InProgress ||
        a.Status == TaskAssignmentStatus.Paused ||
        a.Status == TaskAssignmentStatus.Completed))
        {
            return Result.Failure<TaskAssignment>(TaskErrors.NotAvailableForAssignment);
        }

        // Create a new task assignment
        Result<TaskAssignment> createResult = TaskAssignment.Create(warehouseOperatorId);

        if (createResult.IsFailure)
        {
            return createResult;
        }

        _assignments.Add(createResult.Value);

        // update the status.
        Status = TaskHeaderStatus.Assigned;

        return Result.Success(createResult.Value);
    }

    public Result Start(DateTime modificationDate)
    {
        if (Status != TaskHeaderStatus.Assigned)
        {
            return Result.Failure<TaskAssignment>(TaskErrors.NotAssigned);
        }

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

    public Result<TaskAssignmentLine> AddAssignmentLine(
        Guid? receiptLineId,
        Guid? dispatchLineId,
        Guid fromLocationId,
        Guid toLocationId,
        Guid productId,
        decimal qty)
    {
        // Find the single assignment that is currently in progress
        TaskAssignment taskAssignment = _assignments.SingleOrDefault(a => a.Status is TaskAssignmentStatus.InProgress);

        if (taskAssignment is null)
        {
            return Result.Failure<TaskAssignmentLine>(TaskErrors.HasNoInprogressAssignmet(Id));
        }

        // Attempt to increase the assignment line with the specified parameters
        Result<TaskAssignmentLine> result = taskAssignment.AddLine(
            receiptLineId,
            dispatchLineId,
            fromLocationId,
            toLocationId,
            productId,
            qty);

        return result;
    }

    public Result<TaskLine> AddTaskLine(
        Guid fromLocationId,
        Guid toLocationId,
        Guid productId,
        decimal totalQty)
    {
        // Create a new task line
        var taskLine = TaskLine.Create(
            fromLocationId,
            toLocationId,
            productId,
            totalQty);
            
        _lines.Add(taskLine);
        
        return Result.Success(taskLine);
    }
    
    public Result MapDispatchLineToTaskLine(
        Guid taskLineId,
        Guid dispatchLineId,
        decimal allocatedQty)
    {
        var taskLine = _lines.Find(l => l.Id == taskLineId);
        
        if (taskLine is null)
        {
            return Result.Failure(TaskErrors.TaskLineNotFound(taskLineId));
        }
        
        return taskLine.AddDispatchMapping(dispatchLineId, allocatedQty);
    }
    
    public Result CompleteTaskLine(Guid taskLineId, decimal completedQty)
    {
        if (Status is not TaskHeaderStatus.InProgress)
        {
            return Result.Failure(TaskErrors.NotInProgress);
        }
        
        var taskLine = _lines.Find(l => l.Id == taskLineId);
        
        if (taskLine is null)
        {
            return Result.Failure(TaskErrors.TaskLineNotFound(taskLineId));
        }
        
        Result result = taskLine.CompleteQuantity(completedQty);
        
        if (result.IsFailure)
        {
            return result;
        }
        
        // Check if all task lines are completed
#pragma warning disable S6603
        if (_lines.All(l => l.IsCompleted))
#pragma warning restore S6603
        {
            // Auto-complete the task if all lines are completed
            Status = TaskHeaderStatus.Completed;
        }
        
        return Result.Success();
    }
    
    public bool IsAssignedTo(Guid operatorId)
    {
        var activeAssignment = _assignments.Find(a =>
            a.Status is not TaskAssignmentStatus.Quit or TaskAssignmentStatus.Completed);

        return activeAssignment?.AssignedOperatorId == operatorId;
    }
    
    public TaskLine? FindTaskLineByProductAndLocation(Guid productId, Guid fromLocationId)
    {
        return _lines.Find(l => l.ProductId == productId && l.FromLocationId == fromLocationId);
    }
}
