using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Messaging;
using Crowbond.Modules.Users.Domain.Users;
using Crowbond.Modules.Users.IntegrationEvents;

namespace Crowbond.Modules.Users.Application.Users.UnassignRole;

internal sealed class DriverRoleRemovedDomainEventHandler(IEventBus bus)
    : DomainEventHandler<DriverRoleRemovedDomainEvent>
{
    public override async Task Handle(
        DriverRoleRemovedDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        await bus.PublishAsync(new DriverRoleRemovedIntegrationEvent(
            domainEvent.Id,
            domainEvent.OccurredOnUtc,
            domainEvent.UserId),
            cancellationToken);
    }
}
