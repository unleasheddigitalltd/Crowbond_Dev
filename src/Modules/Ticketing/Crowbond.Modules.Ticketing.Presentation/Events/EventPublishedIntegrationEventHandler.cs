using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Domain;
using Crowbond.Modules.Events.IntegrationEvents;
using Crowbond.Modules.Ticketing.Application.Events.CreateEvent;
using Crowbond.Common.Application.Exceptions;
using MediatR;

namespace Crowbond.Modules.Ticketing.Presentation.Events;

internal sealed class EventPublishedIntegrationEventHandler(ISender sender)
    : IntegrationEventHandler<EventPublishedIntegrationEvent>
{
    public override async Task Handle(
        EventPublishedIntegrationEvent integrationEvent,
        CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(
            new CreateEventCommand(
                integrationEvent.EventId,
                integrationEvent.Title,
                integrationEvent.Description,
                integrationEvent.Location,
                integrationEvent.StartsAtUtc,
                integrationEvent.EndsAtUtc,
                integrationEvent.TicketTypes
                    .Select(t => new CreateEventCommand.TicketTypeRequest(
                        t.Id,
                        integrationEvent.EventId,
                        t.Name,
                        t.Price,
                        t.Currency,
                        t.Quantity))
                    .ToList()),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new CrowbondException(nameof(CreateEventCommand), result.Error);
        }
    }
}
