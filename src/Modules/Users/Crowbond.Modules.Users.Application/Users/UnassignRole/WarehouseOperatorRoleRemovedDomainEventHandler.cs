using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Messaging;
using Crowbond.Modules.Users.Domain.Users;
using Crowbond.Modules.Users.IntegrationEvents;

namespace Crowbond.Modules.Users.Application.Users.UnassignRole;

internal sealed class WarehouseOperatorRoleRemovedDomainEventHandler(IEventBus bus)
    : DomainEventHandler<WarehouseOperatorRoleRemovedDomainEvent>
{
    public override async Task Handle(
        WarehouseOperatorRoleRemovedDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        await bus.PublishAsync(new WarehouseOperatorRoleRemovedIntegrationEvent(
            domainEvent.Id,
            domainEvent.OccurredOnUtc,
            domainEvent.UserId),
            cancellationToken);
    }
}
