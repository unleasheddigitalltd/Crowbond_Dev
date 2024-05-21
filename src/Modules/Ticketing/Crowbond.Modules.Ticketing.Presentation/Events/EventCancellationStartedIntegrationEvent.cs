using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Domain;
using Crowbond.Modules.Events.IntegrationEvents;
using Crowbond.Modules.Ticketing.Application.Events.CancelEvent;
using Crowbond.Common.Application.Exceptions;
using MediatR;

namespace Crowbond.Modules.Ticketing.Presentation.Events;

internal sealed class EventCancellationStartedIntegrationEventHandler(ISender sender)
    : IntegrationEventHandler<EventCancellationStartedIntegrationEvent>
{
    public override async Task Handle(
        EventCancellationStartedIntegrationEvent integrationEvent,
        CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(new CancelEventCommand(integrationEvent.EventId), cancellationToken);

        if (result.IsFailure)
        {
            throw new CrowbondException(nameof(CancelEventCommand), result.Error);
        }
    }
}
