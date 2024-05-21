using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.Ticketing.Application.Tickets.ArchiveTicketsForEvent;
using Crowbond.Modules.Ticketing.Domain.Events;
using Crowbond.Common.Application.Exceptions;
using MediatR;

namespace Crowbond.Modules.Ticketing.Application.Events.CancelEvent;

internal sealed class ArchiveTicketsEventCanceledDomainEventHandler(ISender sender)
    : DomainEventHandler<EventCanceledDomainEvent>
{
    public override async Task Handle(
        EventCanceledDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(new ArchiveTicketsForEventCommand(domainEvent.EventId), cancellationToken);

        if (result.IsFailure)
        {
            throw new CrowbondException(nameof(ArchiveTicketsForEventCommand), result.Error);
        }
    }
}
