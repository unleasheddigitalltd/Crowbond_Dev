using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Messaging;
using Crowbond.Modules.OMS.Domain.Orders;
using Crowbond.Modules.OMS.IntegrationEvents;

namespace Crowbond.Modules.OMS.Application.Orders.AddOrderLine;

internal sealed class OrderLineAddedDomainEventHandler(IEventBus eventBus)
    : DomainEventHandler<OrderLineAddedDomainEvent>
{
    public override async Task Handle(
        OrderLineAddedDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        await eventBus.PublishAsync(
            new OrderLineAddedIntegrationEvent(
                domainEvent.Id,
                domainEvent.OccurredOnUtc,
                domainEvent.CustomerId,
                domainEvent.ProductId),
            cancellationToken);
    }
}
