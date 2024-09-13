using Crowbond.Common.Domain;

namespace Crowbond.Modules.OMS.Domain.RouteTrips;

public sealed class RouteTripStatusHistory : Entity , ITrackable
{
    private RouteTripStatusHistory()
    {        
    }

    public Guid Id { get; private set; }

    public Guid RouteTripId { get; private set; }

    public RouteTripStatus Status { get; private set; }

    public DateTime ChangedAt { get; private set; }

    public Guid ChangedBy { get; set; }

    internal RouteTripStatusHistory(RouteTripStatus status, DateTime changedAt)
    {
        Id = Guid.NewGuid();
        Status = status;
        ChangedAt = changedAt;
    }
}
