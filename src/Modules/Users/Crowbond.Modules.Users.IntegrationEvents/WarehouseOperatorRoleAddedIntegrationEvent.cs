using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Domain;

namespace Crowbond.Modules.Users.IntegrationEvents;

public sealed class WarehouseOperatorRoleAddedIntegrationEvent : IntegrationEvent
{
    public WarehouseOperatorRoleAddedIntegrationEvent(
        Guid id,
        DateTime occurredOnUtc,
        Guid userId)
        : base(id, occurredOnUtc)
    {
        UserId = userId;
    }

    public Guid UserId { get; init; }
}
