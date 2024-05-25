using Crowbond.Common.Domain;

namespace Crowbond.Modules.Users.Domain.Users;
public sealed class UserPasswordResetDomainEvent(Guid eventId) : DomainEvent
{
    public Guid UserId { get; init; } = eventId;
}
