using Crowbond.Common.Domain;

namespace Crowbond.Modules.OMS.Domain.PurchaseOrders;
public static class PurchaseOrderErrors
{
    public static Error NotFound(Guid purchaseOrderId) =>
    Error.NotFound("PurchaseOrders.NotFound", $"The purchase order with the identifier {purchaseOrderId} was not found");

    public static Error FilterTypeNotFound(string filterTypeName) =>
    Error.NotFound("PurchaseOrders.NotFound", $"The filter type with the name {filterTypeName} was not found");

    public static Error SequenceNotFound() =>
    Error.NotFound("Sequence.NotFound", $"The sequence for the purchase order type was not found");
}
