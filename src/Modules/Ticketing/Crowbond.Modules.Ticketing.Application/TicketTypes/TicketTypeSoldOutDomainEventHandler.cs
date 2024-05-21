using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Messaging;
using Crowbond.Modules.Ticketing.Domain.Events;
using Crowbond.Modules.Ticketing.IntegrationEvents;

namespace Crowbond.Modules.Ticketing.Application.TicketTypes;

internal sealed class TicketTypeSoldOutDomainEventHandler(IEventBus eventBus)
    : DomainEventHandler<TicketTypeSoldOutDomainEvent>
{
    public override async Task Handle(
        TicketTypeSoldOutDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        await eventBus.PublishAsync(
            new TicketTypeSoldOutIntegrationEvent(
                domainEvent.Id,
                domainEvent.OccurredOnUtc,
                domainEvent.TicketTypeId),
            cancellationToken);
    }
}
