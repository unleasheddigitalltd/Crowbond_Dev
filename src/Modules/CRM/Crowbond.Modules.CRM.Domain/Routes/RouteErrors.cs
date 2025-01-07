using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Domain.CustomerOutlets;

namespace Crowbond.Modules.CRM.Domain.Routes;

public static class RouteErrors
{
    public static Error NotFound(Guid routeId) =>
    Error.NotFound("Route.NotFound", $"The Route with the identifier {routeId} was not found");

    public static Error DayIsNotAvailable(Guid routeId, Weekday weekday) =>
    Error.NotFound("Route.DayIsNotAvailable", $"The {weekday} is not available for the route with the identifier {routeId}");
}
