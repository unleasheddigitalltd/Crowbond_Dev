namespace Crowbond.Modules.OMS.Application.PurchaseOrders.GetPurchaseOrderWithLines;
public sealed record PurchaseOrderLineResponse(
    Guid PurchaseOrderLineId,
    Guid PurchaseOrderHeaderId,
    Guid ProductId,
    decimal Qty,
    decimal UnitPrice);
