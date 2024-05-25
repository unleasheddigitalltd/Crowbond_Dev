using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Messaging;
using Crowbond.Modules.Users.Domain.Users;
using Crowbond.Modules.Users.IntegrationEvents;

namespace Crowbond.Modules.Users.Application.Users.ResetUserPassword;
internal sealed class UserPasswordResetDomainEventHandler(IEventBus eventBus)
    : DomainEventHandler<UserPasswordResetDomainEvent>
{
    public override async Task Handle(
        UserPasswordResetDomainEvent domainEvent,
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
