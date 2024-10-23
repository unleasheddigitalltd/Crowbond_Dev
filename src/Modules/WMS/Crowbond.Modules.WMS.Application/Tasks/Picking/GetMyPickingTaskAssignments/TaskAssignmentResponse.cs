using Crowbond.Modules.WMS.Domain.Tasks;

namespace Crowbond.Modules.WMS.Application.Tasks.Picking.GetMyPickingTaskAssignments;

public sealed record TaskAssignmentResponse(
    Guid TaskId,
    string TaskNo,
    string DispatchNo,
    string OrderNo,
    string CustomerName,
    TaskAssignmentStatus Status,
    long TotalLines,
    long FinishedLines,
    long ItemTotalLines,
    long ItemFinishedLines,
    long BulkTotalLines,
    long BulkFinishedLines);
