using Crowbond.Common.Application.EventBus;

namespace Crowbond.Modules.Events.IntegrationEvents;

public sealed class EventCanceledIntegrationEvent : IntegrationEvent
{
    public EventCanceledIntegrationEvent(Guid id, DateTime occurredOnUtc, Guid eventId)
        : base(id, occurredOnUtc)
    {
        EventId = eventId;
    }

    public Guid EventId { get; init; }
}
