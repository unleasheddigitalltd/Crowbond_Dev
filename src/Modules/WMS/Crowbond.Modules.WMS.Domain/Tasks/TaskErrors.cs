using Crowbond.Common.Domain;

namespace Crowbond.Modules.WMS.Domain.Tasks;
public static class TaskErrors
{
    public static Error NotFound(Guid taskHeaderId) =>
    Error.NotFound("Tasks.NotFound", $"The task header with the identifier {taskHeaderId} was not found");

    public static Error SequenceNotFound() =>
    Error.NotFound("Tasks.SequenceNotFound", $"The sequence for the task type was not found");
}
