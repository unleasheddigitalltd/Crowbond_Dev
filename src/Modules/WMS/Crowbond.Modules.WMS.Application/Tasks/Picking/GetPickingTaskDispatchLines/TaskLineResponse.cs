namespace Crowbond.Modules.WMS.Application.Tasks.Picking.GetPickingTaskDispatchLines;

public sealed record TaskLineResponse(
    Guid TaskLineId,
    Guid FromLocationId,
    string FromLocationName,
    Guid ToLocationId,
    string ToLocationName,
    Guid ProductId,
    string ProductName,
    string UnitOfMeasure,
    decimal TotalQty,
    decimal CompletedQty,
    bool IsCompleted)
{
    public IReadOnlyCollection<TaskLineDispatchMappingResponse> DispatchMappings { get; init; } = new List<TaskLineDispatchMappingResponse>();
}

public sealed record TaskLineDispatchMappingResponse(
    Guid DispatchLineId,
    decimal AllocatedQty,
    decimal CompletedQty,
    string OrderNo,
    string CustomerBusinessName);

public sealed record EnhancedTaskDispatchLineResponse(
    Guid DispatchLineId,
    Guid ProductId,
    string ProductName,
    string UnitOfMeasure,
    decimal OrderedQty,
    decimal PickedQty,
    bool IsPicked,
    string Location,
    Guid? TaskLineId,
    decimal? TaskLineTotalQty,
    decimal? TaskLineCompletedQty,
    bool? TaskLineIsCompleted);