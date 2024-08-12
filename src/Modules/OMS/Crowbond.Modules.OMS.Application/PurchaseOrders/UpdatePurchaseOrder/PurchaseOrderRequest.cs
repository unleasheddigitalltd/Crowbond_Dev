namespace Crowbond.Modules.OMS.Application.PurchaseOrders.UpdatePurchaseOrder;

public sealed record PurchaseOrderRequest(
    DateOnly RequiredDate,
    string? PurchaseOrderNotes,
    List<PurchaseOrderRequest.PurchaseOrderLine> PurchaseOrderLines)
{
    public sealed record PurchaseOrderLine(
        Guid ProductId,
        decimal Qty,
        string? Comments);
}
