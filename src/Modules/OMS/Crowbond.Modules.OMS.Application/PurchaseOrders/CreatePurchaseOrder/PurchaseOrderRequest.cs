namespace Crowbond.Modules.OMS.Application.PurchaseOrders.CreatePurchaseOrder;

public sealed record PurchaseOrderRequest(
    Guid SupplierId,
    DateOnly RequiredDate,
    string? PurchaseOrderNotes,
    List<PurchaseOrderRequest.PurchaseOrderLine> PurchaseOrderLines)
{
    public sealed record PurchaseOrderLine(
        Guid ProductId,
        decimal Qty,
        string? Comments);
}


