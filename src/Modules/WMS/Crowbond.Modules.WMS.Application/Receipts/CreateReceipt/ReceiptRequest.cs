namespace Crowbond.Modules.WMS.Application.Receipts.CreateReceipt;

public sealed record ReceiptRequest(
    DateTime ReceivedDate,
    Guid PurchaseOrderId,
    string? PurchaseOrderNo,
    string DeliveryNoteNumber,
    List<ReceiptRequest.ReceiptLineRequest> ReceiptLines)
{
    public sealed record ReceiptLineRequest(
        Guid ProductId,
        decimal QuantityReceived,
        decimal UnitPrice,
        DateOnly? SellByDate,
        DateOnly? UseByDate,
        string BatchNumber);       
}
