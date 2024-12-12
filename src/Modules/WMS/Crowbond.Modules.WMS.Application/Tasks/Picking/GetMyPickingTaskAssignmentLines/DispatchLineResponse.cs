namespace Crowbond.Modules.WMS.Application.Tasks.Picking.GetMyPickingTaskAssignmentLines;

public sealed record DispatchLineResponse(
    Guid DispatchLineId,
    string ProductSku,
    string ProductName,
    bool IsPicked);
