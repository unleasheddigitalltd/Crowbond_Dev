using Crowbond.Common.Domain;

namespace Crowbond.Modules.WMS.Domain.Receipts;
public static class ReceiptErrors
{
    public static Error NotFound(Guid receiptId) =>
    Error.NotFound("Receipts.NotFound", $"The receipt header with the identifier {receiptId} was not found");

    public static Error PurchaseOrderNotFound(Guid purchaseOrderId) =>
        Error.NotFound("Receipts.NotFound", $"The receipt header for the purchase order identifier {purchaseOrderId} was not found");

    public static Error HasNoLines(Guid receiptId) =>
        Error.NotFound("Receipts.HasNoLines", $"The receipt header with the identifier {receiptId} has no lines");

    public static Error LineNotFound(Guid receiptLineId) =>
        Error.NotFound("Receipts.LineNotFound", $"The receipt line with the identifier {receiptLineId} was not found");

    public static Error HasNoLineForProduct(Guid productId) => 
        Error.NotFound("Receipts.HasNoLineForProduct", $"The receipt has no line for product with the identifier {productId}");
    public static Error SequenceNotFound() =>
        Error.NotFound("Receipts.SequenceNotFound", $"The sequence for the receipt type was not found");

    public static readonly Error NotShipping = Error.Problem("Receipts.NotShipping", "The receipt is not in shipping status");
}
