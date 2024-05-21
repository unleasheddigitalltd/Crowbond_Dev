using Crowbond.Common.Application.EventBus;

namespace Crowbond.Modules.Events.IntegrationEvents;

public sealed class EventCancellationStartedIntegrationEvent : IntegrationEvent
{
    public EventCancellationStartedIntegrationEvent(Guid id, DateTime occurredOnUtc, Guid eventId)
        : base(id, occurredOnUtc)
    {
        EventId = eventId;
    }

    public Guid EventId { get; init; }
}
