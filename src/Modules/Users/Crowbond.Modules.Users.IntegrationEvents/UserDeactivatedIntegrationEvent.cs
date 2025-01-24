using Crowbond.Common.Application.EventBus;

namespace Crowbond.Modules.Users.IntegrationEvents;

public sealed class UserDeactivatedIntegrationEvent : IntegrationEvent
{
    public UserDeactivatedIntegrationEvent(
        Guid id,
        DateTime occurredOnUtc,
        Guid userId)
        : base(id, occurredOnUtc)
    {
        UserId = userId;
    }

    public Guid UserId { get; init; }
}
