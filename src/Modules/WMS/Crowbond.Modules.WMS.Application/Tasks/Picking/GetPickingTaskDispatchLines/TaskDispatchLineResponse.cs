namespace Crowbond.Modules.WMS.Application.Tasks.Picking.GetPickingTaskDispatchLines;

public sealed record TaskDispatchLineResponse(
    Guid DispatchLineId,
    string ProductName,
    decimal OrderedQty,
    decimal PickedQty,
    bool IsPicked);
