using Crowbond.Common.Domain;

namespace Crowbond.Modules.OMS.Domain.PurchaseOrderHeaders;
public static class PurchaseOrderHeaderErrors
{
    public static Error NotFound(Guid purchaseOrderId) =>
    Error.NotFound("PurchaseOrders.NotFound", $"The purchase order with the identifier {purchaseOrderId} was not found");

    public static Error SequenceNotFound() =>
    Error.NotFound("Sequence.NotFound", $"The sequence for the purchase order type was not found");

    public static readonly Error NotDraft = Error.Problem("PurchaseOrders.NotDraft", "The purchase order is not in draft status");

    public static readonly Error NotPending = Error.Problem("PurchaseOrders.NotPending", "The purchase order is not in pending status");

    public static readonly Error AlreadyPaid = Error.Problem(
        "PurchaseOrders.AlreadyPaid",
        "The purchase order was already paid");
}
