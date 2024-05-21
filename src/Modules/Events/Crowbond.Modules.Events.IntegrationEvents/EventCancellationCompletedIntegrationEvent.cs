using Crowbond.Common.Application.EventBus;

namespace Crowbond.Modules.Events.IntegrationEvents;

public sealed class EventCancellationCompletedIntegrationEvent : IntegrationEvent
{
    public EventCancellationCompletedIntegrationEvent(Guid id, DateTime occurredOnUtc, Guid eventId)
        : base(id, occurredOnUtc)
    {
        EventId = eventId;
    }

    public Guid EventId { get; init; }
}
