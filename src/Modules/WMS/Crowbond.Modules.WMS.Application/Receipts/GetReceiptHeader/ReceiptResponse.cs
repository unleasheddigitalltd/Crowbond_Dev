namespace Crowbond.Modules.WMS.Application.Receipts.GetReceiptHeader;

public sealed record ReceiptResponse(
    Guid Id,
    DateTime ReceivedDate,
    string PurchaseOrderNumber,
    string DeliveryNoteNumber,
    string Status);
