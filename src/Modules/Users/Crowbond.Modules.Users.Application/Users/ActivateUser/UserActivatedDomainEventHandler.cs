using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Messaging;
using Crowbond.Modules.Users.Domain.Users;
using Crowbond.Modules.Users.IntegrationEvents;

namespace Crowbond.Modules.Users.Application.Users.ActivateUser;

internal sealed class UserActivatedDomainEventHandler(IEventBus bus)
    : DomainEventHandler<UserActivatedDomainEvent>
{
    public override async Task Handle(
        UserActivatedDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        await bus.PublishAsync(
            new UserActivatedIntegrationEvent(
                domainEvent.Id,
                domainEvent.OccurredOnUtc,
                domainEvent.UserId),
            cancellationToken);
    }
}
