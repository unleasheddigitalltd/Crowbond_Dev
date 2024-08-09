using Crowbond.Common.Domain;

namespace Crowbond.Modules.OMS.Domain.RouteTrips;

public sealed class RouteTrip : Entity
{
    private RouteTrip()
    {        
    }

    public Guid Id { get; private set; }

    public DateOnly Date {  get; private set; }

    public Guid RouteId { get; private set; }

    public string? Comments { get; private set; }

    public RouteTripStatus Status { get; private set; }

    public static RouteTrip Create(DateOnly date, Guid routeId, string comments)
    {
        var routeTrip = new RouteTrip
        {
            Id = Guid.NewGuid(),
            Date = date,
            RouteId = routeId,
            Comments = comments,
            Status = RouteTripStatus.Registered
        };

        return routeTrip;
    }


}
