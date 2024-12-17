using Crowbond.Common.Domain;

namespace Crowbond.Modules.Users.Domain.Users;

public sealed class DriverRoleAddedDomainEvent(Guid userId) : DomainEvent
{
    public Guid UserId { get; init; } = userId;
}
