using Crowbond.Common.Domain;

namespace Crowbond.Modules.OMS.Domain.RouteTrips;

public static class RouteTripErrors
{
    public static Error NotFound(Guid routeTripId) =>
    Error.NotFound("RouteTrips.NotFound", $"The route trip with the identifier {routeTripId} was not found");
    
    public static Error InvalidStatus(RouteTripStatus status) =>
    Error.Problem("RouteTrips.NotAvailable", $"The route trip was not in '{status}' status");
    public static Error Expired(Guid routeTripId) =>
    Error.Problem("RouteTrips.Expired", $"The route trip with the identifier {routeTripId} has expired");

    public readonly static Error DateInPast =
    Error.Problem("Date.DateInPast", $"The provided date cannot be in the past.");

}
