using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Messaging;
using Crowbond.Modules.Users.Domain.Users;
using Crowbond.Modules.Users.IntegrationEvents;

namespace Crowbond.Modules.Users.Application.Users.DeactivateUser;

internal sealed class UserDeactivatedDomainEventHandler(IEventBus bus)
    : DomainEventHandler<UserDeactivatedDomainEvent>
{
    public override async Task Handle(
        UserDeactivatedDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        await bus.PublishAsync(
            new UserDeactivatedIntegrationEvent(
                domainEvent.Id,
                domainEvent.OccurredOnUtc,
                domainEvent.UserId),
            cancellationToken);
    }
}
