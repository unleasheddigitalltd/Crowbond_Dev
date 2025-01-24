namespace Crowbond.Modules.WMS.Application.Receipts.GetReceiptHeader;

public sealed record ReceiptResponse(
    Guid Id,
    string ReceiptNo,
    DateTime? ReceivedDate,
    string? PurchaseOrderNo,
    string? DeliveryNoteNumber,
    string Status);
