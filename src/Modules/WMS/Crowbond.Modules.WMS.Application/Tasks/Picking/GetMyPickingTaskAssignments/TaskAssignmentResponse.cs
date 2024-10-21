using Crowbond.Modules.WMS.Domain.Tasks;

namespace Crowbond.Modules.WMS.Application.Tasks.Picking.GetMyPickingTaskAssignments;

public sealed record TaskAssignmentResponse(
    Guid TaskId,
    string TaskNo,
    TaskAssignmentStatus Status,
    long TotalLines,
    long FinishedLines);
