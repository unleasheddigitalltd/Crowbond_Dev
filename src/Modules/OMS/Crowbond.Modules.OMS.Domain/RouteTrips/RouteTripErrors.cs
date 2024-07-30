using Crowbond.Common.Domain;

namespace Crowbond.Modules.OMS.Domain.RouteTrips;

public static class RouteTripErrors
{
    public static Error NotFound(Guid driverId) =>
    Error.NotFound("Driver.NotFound", $"The driver with the identifier {driverId} was not found");

    public static Error DateInPast() =>
    Error.Problem("Date.InPast", $"The provided date cannot be in the past.");
}
