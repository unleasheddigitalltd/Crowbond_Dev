using Crowbond.Common.Domain;

namespace Crowbond.Modules.OMS.Domain.Routes;

public static class RouteErrors
{
    public static Error NotFound(Guid routeId) =>
    Error.NotFound("Route.NotFound", $"The Route with the identifier {routeId} was not found");
}
