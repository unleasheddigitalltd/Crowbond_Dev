using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Messaging;
using Crowbond.Modules.Users.Domain.Users;
using Crowbond.Modules.Users.IntegrationEvents;

namespace Crowbond.Modules.Users.Application.Users.AssignRole;

internal sealed class WarehouseOperatorRoleAddedDomainEventHandler(IEventBus bus)
    : DomainEventHandler<WarehouseOperatorRoleAddedDomainEvent>
{
    public override async Task Handle(
        WarehouseOperatorRoleAddedDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        await bus.PublishAsync(new WarehouseOperatorRoleAddedIntegrationEvent(
            domainEvent.Id,
            domainEvent.OccurredOnUtc,
            domainEvent.UserId),
            cancellationToken);
    }
}
