using Crowbond.Common.Application.EventBus;

namespace Crowbond.Modules.CRM.IntegrationEvents;

public sealed class CustomerContactDeactivatedIntegrationEvent: IntegrationEvent
{
    public CustomerContactDeactivatedIntegrationEvent(
        Guid id,
        DateTime occurredOnUtc,
        Guid userId)
        : base(id, occurredOnUtc)
    {
        UserId = userId;
    }

    public Guid UserId { get; init; }
}
