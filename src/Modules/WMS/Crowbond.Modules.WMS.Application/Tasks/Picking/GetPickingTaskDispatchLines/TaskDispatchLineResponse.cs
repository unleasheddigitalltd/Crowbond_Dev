namespace Crowbond.Modules.WMS.Application.Tasks.Picking.GetPickingTaskDispatchLines;

public sealed record TaskDispatchLineResponse(
    Guid DispatchLineId,
    Guid ProductId,
    string ProductName,
    string UnitOfMeasure,
    decimal OrderedQty,
    decimal PickedQty,
    bool IsPicked,
    string Location);
