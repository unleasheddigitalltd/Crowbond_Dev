namespace Crowbond.Modules.WMS.Application.Receipts.GetReceiptHeader;

public sealed record ReceiptResponse(
    Guid Id,
    DateTime ReceivedDate,
    string PurchaseOrderNo,
    string DeliveryNoteNumber,
    string Status);
