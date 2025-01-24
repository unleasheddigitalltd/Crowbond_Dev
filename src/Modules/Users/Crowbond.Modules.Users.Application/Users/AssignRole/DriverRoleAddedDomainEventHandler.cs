using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Messaging;
using Crowbond.Modules.Users.Domain.Users;
using Crowbond.Modules.Users.IntegrationEvents;

namespace Crowbond.Modules.Users.Application.Users.AssignRole;

internal sealed class DriverRoleAddedDomainEventHandler(IEventBus bus)
    : DomainEventHandler<DriverRoleAddedDomainEvent>
{
    public override async Task Handle(
        DriverRoleAddedDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        await bus.PublishAsync(new DriverRoleAddedIntegrationEvent(
            domainEvent.Id,
            domainEvent.OccurredOnUtc,
            domainEvent.UserId),
            cancellationToken);
    }
}
