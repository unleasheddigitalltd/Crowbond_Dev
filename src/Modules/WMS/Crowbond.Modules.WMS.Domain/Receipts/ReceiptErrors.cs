using Crowbond.Common.Domain;

namespace Crowbond.Modules.WMS.Domain.Receipts;
public static class ReceiptErrors
{
    public static Error NotFound(Guid receiptId) =>
    Error.NotFound("Receipts.NotFound", $"The receipt header with the identifier {receiptId} was not found");

    public static Error PurchaseOrderNotFound(Guid purchaseOrderId) =>
        Error.NotFound("Receipts.NotFound", $"The receipt header for the purchase order identifier {purchaseOrderId} was not found");

    public static readonly Error NotShipping = Error.Problem("Receipts.NotShipping", "The receipt is not in shipping status");
}
