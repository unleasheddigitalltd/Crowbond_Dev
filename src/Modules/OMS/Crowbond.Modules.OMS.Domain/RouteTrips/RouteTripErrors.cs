using Crowbond.Common.Domain;

namespace Crowbond.Modules.OMS.Domain.RouteTrips;

public static class RouteTripErrors
{
    public static Error NotFound(Guid routeTrip) =>
    Error.NotFound("RouteTrips.NotFound", $"The route trip with the identifier {routeTrip} was not found");

    public static Error DateInPast() =>
    Error.Problem("Date.InPast", $"The provided date cannot be in the past.");
}
