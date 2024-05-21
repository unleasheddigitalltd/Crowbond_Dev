using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Domain;
using Crowbond.Modules.Attendance.Application.Events.CreateEvent;
using Crowbond.Modules.Events.IntegrationEvents;
using Crowbond.Common.Application.Exceptions;
using MediatR;

namespace Crowbond.Modules.Attendance.Presentation.Events;

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
                integrationEvent.EndsAtUtc),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new CrowbondException(nameof(CreateEventCommand), result.Error);
        }
    }
}
