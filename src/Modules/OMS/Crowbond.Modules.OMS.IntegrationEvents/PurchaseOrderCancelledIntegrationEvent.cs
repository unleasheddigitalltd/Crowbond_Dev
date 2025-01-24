using Crowbond.Common.Application.EventBus;

namespace Crowbond.Modules.OMS.IntegrationEvents;

public sealed class PurchaseOrderCancelledIntegrationEvent : IntegrationEvent
{
    public PurchaseOrderCancelledIntegrationEvent(
        Guid id,
        DateTime occurredOnUtc,
        Guid purchaseOrderId)
        : base(id, occurredOnUtc)
    {
        PurchaseOrderId = purchaseOrderId;
    }

    public Guid PurchaseOrderId { get; init; }

}
