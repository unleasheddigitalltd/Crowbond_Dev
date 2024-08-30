using Crowbond.Common.Domain;

namespace Crowbond.Modules.WMS.Domain.Tasks;
public static class TaskErrors
{
    public static Error NotFound(Guid taskHeaderId) =>
    Error.NotFound("Tasks.NotFound", $"The task with the identifier {taskHeaderId} was not found");

    public static Error HasNoPendingAssignment(Guid taskHeaderId) =>
    Error.NotFound("Tasks.HasNoPendingAssignment", $"The task with the identifier {taskHeaderId} has no pending assignment");

    public static Error HasInProggressAssignment(Guid taskHeaderId) =>
    Error.NotFound("Tasks.HasInProggressAssignment", $"The task with the identifier {taskHeaderId} already has an in progress assignment");

    public static Error SequenceNotFound() =>
    Error.NotFound("Tasks.SequenceNotFound", $"The sequence for the task type was not found");

    public static readonly Error alreadyStarted = 
        Error.Problem("Tasks.TaskAssinmentIsStarted", "The task assignment was already started");


    
}
