using Crowbond.Common.Domain;

namespace Crowbond.Modules.OMS.Domain.PurchaseOrders;
public static class PurchaseOrderErrors
{
    public static Error NotFound(Guid purchaseOrderId) =>
    Error.NotFound("PurchaseOrders.NotFound", $"The purchase order with the identifier {purchaseOrderId} was not found");
    
    public static Error LineNotFound(Guid purchaseOrderLineId) =>
    Error.NotFound("PurchaseOrders.LineNotFound", $"The purchase order line with the identifier {purchaseOrderLineId} was not found");
    
    public static Error LineWithSameProductAlreadyExists(Guid productId) =>
    Error.Conflict("PurchaseOrders.LineWithSameProductAlreadyExists", $"A purchase order line with the product identifier {productId} already exists.");

    public static Error SequenceNotFound() =>
    Error.NotFound("Sequence.NotFound", $"The sequence for the purchase order type was not found");

    public static readonly Error NotDraft = Error.Problem("PurchaseOrders.NotDraft", "The purchase order is not in draft status");

    public static readonly Error NotPending = Error.Problem("PurchaseOrders.NotPending", "The purchase order is not in pending status");

    public static readonly Error NotApproved = Error.Problem("PurchaseOrders.NotApproved", "The purchase order is not in approved status");

    public static readonly Error AlreadyPaid = Error.Problem("PurchaseOrders.AlreadyPaid", "The purchase order was already paid");
}
