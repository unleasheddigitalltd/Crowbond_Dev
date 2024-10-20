using Crowbond.Common.Domain;

namespace Crowbond.Modules.WMS.Domain.Tasks;
public static class TaskErrors
{
    public static Error NotFound(Guid taskHeaderId) =>
    Error.NotFound("Tasks.NotFound", $"The task with the identifier {taskHeaderId} was not found");
    
    public static Error AssignmentLineNotFound(Guid taskAssignmentLineId) =>
    Error.NotFound("Tasks.AssignmentLineNotFound", $"The task assignment line with the identifier {taskAssignmentLineId} was not found");

    public static Error HasNoPendingAssignment(Guid taskHeaderId) =>
        Error.NotFound("Tasks.HasNoPendingAssignment", $"The task with the identifier {taskHeaderId} has no pending assignment");

    public static Error HasInProggressAssignment(Guid taskHeaderId) =>
        Error.NotFound("Tasks.HasInProggressAssignment", $"The task with the identifier {taskHeaderId} already has an in progress assignment");

    public static Error SequenceNotFound() =>
        Error.NotFound("Tasks.SequenceNotFound", $"The sequence for the task type was not found");

    public static readonly Error NotAssigned =
        Error.Problem("Tasks.NotAssigned", "The task was not assigned");

    public static readonly Error AlreadyAssigned =
        Error.Problem("Tasks.AlreadyAssigned", "The task was already assigned");

    public static readonly Error AlreadyStarted = 
        Error.Problem("Tasks.AlreadyStarted", "The task was already started");

    public static readonly Error IsStarted =
        Error.Problem("Tasks.IsStarted", "The task was was started");

    public static readonly Error NotInProgress =
        Error.Problem("Tasks.NotInProgress", "The task was not in progress");

    public static readonly Error AlreadyInprogress = 
        Error.Problem("Tasks.AlreadyInprogress", "The task was already in progress");

    public static readonly Error IsInProgress = 
        Error.Problem("Tasks.IsInProgress", "The task was in progress");

    public static readonly Error IsCompleted = 
        Error.Problem("Tasks.IsCompleted", "The task was completed");

    public static readonly Error IsCanceled = 
        Error.Problem("Tasks.IsCanceled", "The task was canceled");

    public static readonly Error LineIsCompleted = 
        Error.Problem("Tasks.LineIsCompleted", "The task line was completed");
    
    public static readonly Error LineNotInProgress = 
        Error.Problem("Tasks.LineNotInProgress", "The task assignment line is not in in-progress status");

    public static readonly Error QuantityNotGreaterThanZero =
        Error.Problem("Tasks.QuantityNotGreaterThanZero", "The quantity should be greater than zero");

    public static Error UnknownStatus(TaskHeaderStatus status) => 
        Error.Problem("Tasks.UnknownStatus", $"The task is in an unknown status: {status}.");

    public static Error LineForProductNotFound(Guid productId) => 
        Error.NotFound("Tasks.LineForProductNotFound", $"The task assignment line for product with the identifier {productId} was not found");
    
    public static Error ActiveAssignmentForOperatorNotFound(Guid operatorId) => 
        Error.NotFound("Tasks.ActiveAssignmentForOperatorNotFound", $"The active task assignment for operator with the identifier {operatorId} was not found");


    public static Error HasNoInprogressAssignmet(Guid taskHeaderId) =>
        Error.NotFound("Tasks.HasNoInprogressAssignmet", $"The task with the identifier {taskHeaderId} has no in-progress task assignment");

    public static Error HasNoPausedAssignmet(Guid taskHeaderId) =>
        Error.NotFound("Tasks.HasNoPausedAssignmet", $"The task with the identifier {taskHeaderId} has no paused task assignment");

    public static Error LineForProductIsNotInProgress(Guid productId) => 
        Error.Problem("Tasks.LineForProductIsNotInProgress", $"The task assignment line for product with the identifier {productId} was not in-progress");

    public static Error ProductCompleteQtyExceedsRequestQty(Guid productId) =>
        Error.Problem("Tasks.ProductCompleteQtyExceedsRequestQty", $"The task assignment line for the product with identifier {productId} has a completed quantity greater than the requested quantity.");
}
