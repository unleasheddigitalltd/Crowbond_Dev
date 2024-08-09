using Crowbond.Common.Domain;

namespace Crowbond.Modules.OMS.Domain.PurchaseOrderLines;

public static class PurchaseOrderLineErrors
{
    public static Error NotFound(Guid purchaseOrderLineId) =>
    Error.NotFound("PurchaseOrderLines.NotFound", $"The purchase order line with the identifier {purchaseOrderLineId} was not found");
}
