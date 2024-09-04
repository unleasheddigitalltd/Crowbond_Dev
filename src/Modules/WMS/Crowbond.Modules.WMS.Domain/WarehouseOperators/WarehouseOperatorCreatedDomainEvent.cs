using Crowbond.Common.Domain;

namespace Crowbond.Modules.WMS.Domain.WarehouseOperators;
public sealed class WarehouseOperatorCreatedDomainEvent(Guid operatorId) : DomainEvent
{
    public Guid OperatorId { get; init; } = operatorId;
}
