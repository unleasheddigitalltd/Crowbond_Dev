using Crowbond.Common.Domain;

namespace Crowbond.Modules.OMS.Domain.PurchaseOrders;

public sealed class PurchaseOrderCancelledDomainEvent(Guid purchaseOrderHeaderId) : DomainEvent
{
    public Guid PurchaseOrderHeaderId { get; init; } = purchaseOrderHeaderId;
}
