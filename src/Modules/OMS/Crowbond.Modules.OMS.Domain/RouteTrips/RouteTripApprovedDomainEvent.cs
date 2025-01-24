using Crowbond.Common.Domain;

namespace Crowbond.Modules.OMS.Domain.RouteTrips;

public sealed class RouteTripApprovedDomainEvent(Guid routeTripId) : DomainEvent
{
    public Guid RouteTripId { get; init; } = routeTripId;
}
