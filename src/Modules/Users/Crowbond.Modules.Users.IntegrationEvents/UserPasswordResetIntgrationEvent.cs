using Crowbond.Common.Application.EventBus;

namespace Crowbond.Modules.Users.IntegrationEvents;
public class UserPasswordResetIntgrationEvent : IntegrationEvent
{
    public UserPasswordResetIntgrationEvent(Guid id, DateTime occurredOnUtc, Guid eventId)
        : base(id, occurredOnUtc)
    {
        EventId = eventId;
    }
    public Guid EventId { get; init; }
   
}
