using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Messaging;
using Crowbond.Modules.Ticketing.Domain.Events;
using Crowbond.Modules.Ticketing.IntegrationEvents;

namespace Crowbond.Modules.Ticketing.Application.Tickets.ArchiveTicketsForEvent;

internal sealed class EventTicketsArchivedDomainEventHandler(IEventBus eventBus)
     : DomainEventHandler<EventTicketsArchivedDomainEvent>
{
    public override async Task Handle(
        EventTicketsArchivedDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        await eventBus.PublishAsync(
            new EventTicketsArchivedIntegrationEvent(
                domainEvent.EventId,
                domainEvent.OccurredOnUtc,
                domainEvent.EventId),
            cancellationToken);
    }
}
