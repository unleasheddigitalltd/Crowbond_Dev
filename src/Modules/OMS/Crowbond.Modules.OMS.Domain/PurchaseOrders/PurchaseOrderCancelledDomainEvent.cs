using Crowbond.Common.Domain;

namespace Crowbond.Modules.OMS.Domain.PurchaseOrders;

public sealed class PurchaseOrderCancelledDomainEvent(Guid purchaseOrderHeaderId, Guid userId, DateTime dateTime) : DomainEvent
{
    public Guid PurchaseOrderHeaderId { get; init; } = purchaseOrderHeaderId;

    public Guid UserId { get; init; } = userId;

    public DateTime? DateTime { get; init; } = dateTime;
}
