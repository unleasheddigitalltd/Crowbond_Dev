using Crowbond.Common.Application.EventBus;

namespace Crowbond.Modules.OMS.IntegrationEvents;

public sealed class PurchaseOrderCancelledIntegrationEvent : IntegrationEvent
{
    public PurchaseOrderCancelledIntegrationEvent(
        Guid id,
        DateTime occurredOnUtc,
        Guid purchaseOrderId,
        Guid userId,
        DateTime utcNow)
        : base(id, occurredOnUtc)
    {
        PurchaseOrderId = purchaseOrderId;
        UserId = userId;
        UtcNow = utcNow;
    }

    public Guid PurchaseOrderId { get; init; }

    public Guid UserId { get; init; }

    public DateTime UtcNow { get; init; }

}
