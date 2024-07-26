using Crowbond.Common.Domain;

namespace Crowbond.Modules.OMS.Domain.RouteTrips;

public sealed class RouteTripStatusHistory : Entity
{
    public RouteTripStatusHistory()
    {        
    }

    public Guid Id { get; private set; }

    public Guid RouteTripId { get; private set; }

    public RouteTripStatus Status { get; private set; }

    public DateTime ChangedAt { get; private set; }

    public Guid ChangedBy { get; private set; }

    public static RouteTripStatusHistory Create(
        Guid routeTripId, 
        RouteTripStatus status, 
        DateTime changedAt, 
        Guid changedBy)
    {
        var RouteTripStatusHistory = new RouteTripStatusHistory
        {
            Id = Guid.NewGuid(),
            RouteTripId = routeTripId,
            Status = status,
            ChangedAt = changedAt,
            ChangedBy = changedBy
        };

        return RouteTripStatusHistory;
    }
}
