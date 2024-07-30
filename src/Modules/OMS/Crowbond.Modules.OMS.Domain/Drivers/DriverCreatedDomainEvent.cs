using Crowbond.Common.Domain;

namespace Crowbond.Modules.OMS.Domain.Drivers;

public sealed class DriverCreatedDomainEvent(Guid driverId) : DomainEvent
{
    public Guid DriverId { get; init; } = driverId;
}
