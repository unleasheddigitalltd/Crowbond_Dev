namespace Crowbond.Modules.WMS.Application.Dispatches.AddDispatchLines;

public sealed record DispatchLineRequest(
    Guid OrderId,
    string OrderNo,
    string CustomerBusinessName,
    Guid OrderLineId,
    Guid ProductId,
    decimal Qty,
    bool IsBulk);
