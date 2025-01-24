using Crowbond.Common.Application.EventBus;

namespace Crowbond.Modules.Users.IntegrationEvents;

public sealed class DriverRoleAddedIntegrationEvent : IntegrationEvent
{
    public DriverRoleAddedIntegrationEvent(
        Guid id,
        DateTime occurredOnUtc,
        Guid userId)
        : base(id, occurredOnUtc)
    {
        UserId = userId;
    }

    public Guid UserId { get; init; }
}
