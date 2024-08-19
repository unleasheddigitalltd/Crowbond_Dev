using Crowbond.Common.Domain;

namespace Crowbond.Modules.OMS.Domain.PurchaseOrders;
public sealed class PurchaseOrderApprovedDomainEvent : DomainEvent
{
    public PurchaseOrderApprovedDomainEvent(Guid purchaseOrderId)
    {
        PurchaseOrderId = purchaseOrderId;
    }

    public Guid PurchaseOrderId { get; init; } 
}
