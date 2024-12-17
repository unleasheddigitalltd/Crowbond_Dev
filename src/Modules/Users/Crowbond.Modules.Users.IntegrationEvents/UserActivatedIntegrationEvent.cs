using Crowbond.Common.Application.EventBus;

namespace Crowbond.Modules.Users.IntegrationEvents;

public sealed class UserActivatedIntegrationEvent: IntegrationEvent
{
    public UserActivatedIntegrationEvent(
        Guid id,
        DateTime occurredOnUtc, 
        Guid userId)
        : base(id, occurredOnUtc)
    {
        UserId = userId;
    }

    public Guid UserId { get; init; }
}
