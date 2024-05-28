using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Messaging;
using Crowbond.Modules.Users.Domain.Users;
using Crowbond.Modules.Users.IntegrationEvents;

namespace Crowbond.Modules.Users.Application.Users.LogOutUser;
internal sealed class UserLogedOutDomainEventHandler(IEventBus eventBus)
    : DomainEventHandler<UserPasswordLogedOutDomainEvent>
{
    public override async Task Handle(
        UserPasswordLogedOutDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        await eventBus.PublishAsync(
            new UserPasswordResetIntgrationEvent(
                domainEvent.Id,
                domainEvent.OccurredOnUtc,
                domainEvent.UserId),
            cancellationToken);
    }
}
