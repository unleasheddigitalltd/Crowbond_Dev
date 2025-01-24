using Crowbond.Common.Domain;

namespace Crowbond.Modules.Users.Domain.Users;

public sealed class WarehouseOperatorRoleAddedDomainEvent(Guid userId) : DomainEvent
{
    public Guid UserId { get; init; } = userId;
}
