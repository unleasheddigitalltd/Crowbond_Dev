using Crowbond.Common.Application.EventBus;

namespace Crowbond.Modules.OMS.IntegrationEvents;

public sealed class OrderLineAddedIntegrationEvent : IntegrationEvent
{
    public OrderLineAddedIntegrationEvent(
        Guid id,
        DateTime occurredOnUtc, 
        Guid customerId,
        Guid productId)
        : base(id, occurredOnUtc)
    {
        CustomerId = customerId;
        ProductId = productId;
    }

    public Guid CustomerId { get; init; }
    public Guid ProductId { get; init; }
}
