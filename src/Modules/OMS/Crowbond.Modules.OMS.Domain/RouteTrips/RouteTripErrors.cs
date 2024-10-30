using Crowbond.Common.Domain;

namespace Crowbond.Modules.OMS.Domain.RouteTrips;

public static class RouteTripErrors
{
    public static Error NotFound(Guid routeTripId) =>
    Error.NotFound("RouteTrips.NotFound", $"The route trip with the identifier {routeTripId} was not found");
    
    public static Error NotAvailable(Guid routeTripId) =>
    Error.Problem("RouteTrips.NotAvailable", $"The route trip with the identifier {routeTripId} was not available");
    public static Error Expired(Guid routeTripId) =>
    Error.Problem("RouteTrips.Expired", $"The route trip with the identifier {routeTripId} has expired");

    public static Error DateInPast() =>
    Error.Problem("Date.InPast", $"The provided date cannot be in the past.");
}
