using Crowbond.Common.Domain;

namespace Crowbond.Modules.WMS.Domain.Receipts;
public static class ReceiptErrors
{
    public static Error NotFound(Guid receiptId) =>
    Error.NotFound("Receipts.NotFound", $"The receipt header with the identifier {receiptId} was not found");
    
    public static Error LineNotFound(Guid receiptLineId) =>
    Error.NotFound("Receipts.LineNotFound", $"The receipt line with the identifier {receiptLineId} was not found");

    public static Error PurchaseOrderNotFound(Guid purchaseOrderId) =>
        Error.NotFound("Receipts.NotFound", $"The receipt header for the purchase order identifier {purchaseOrderId} was not found");

    public static Error HasNoLines(Guid receiptId) =>
        Error.NotFound("Receipts.HasNoLines", $"The receipt header with the identifier {receiptId} has no lines");

    public static Error SequenceNotFound() =>
        Error.NotFound("Receipts.SequenceNotFound", $"The sequence for the receipt type was not found");

    public static readonly Error NotShipping = Error.Problem("Receipts.NotShipping", "The receipt is not in shipping status");

    public static readonly Error NotReceived = Error.Problem("Receipts.NotReceived", "The receipt is not in received status");

    public static readonly Error StoredExceedsReceived = Error.Problem("Receipts.StoredExceedsReceived", "The recorded stored quantity exceeds the actual received quantity");

    public static readonly Error LineAlreadyStored = Error.Problem("Receipts.LineAlreadyStored", "The selected receipt line has already been marked as stored in the warehouse and cannot be processed further.");
}
