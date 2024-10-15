namespace Crowbond.Modules.WMS.Application.Receipts.CreateReceipt;

public sealed record ReceiptRequest(
    Guid PurchaseOrderId,
    string? PurchaseOrderNo,
    List<ReceiptRequest.ReceiptLineRequest> ReceiptLines)
{
    public sealed record ReceiptLineRequest(
        Guid ProductId,
        decimal QuantityReceived,
        decimal UnitPrice);       
}
