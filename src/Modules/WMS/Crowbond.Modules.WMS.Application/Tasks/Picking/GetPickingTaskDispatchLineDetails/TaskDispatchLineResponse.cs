using Crowbond.Modules.WMS.Domain.Tasks;

namespace Crowbond.Modules.WMS.Application.Tasks.Picking.GetPickingTaskDispatchLineDetails;

public sealed record TaskDispatchLineResponse(
    Guid Id,
    Guid TaskId,
    TaskType TaskType,
    Guid ProductId,
    string ProductName,
    string UnitOfMeasure,
    decimal OrderedQty,
    decimal PickedQty,
    string CustomerBusinessName,
    Guid OrderId,
    Guid OrderLineId,
    string OrderNo,
    bool IsPicked);
