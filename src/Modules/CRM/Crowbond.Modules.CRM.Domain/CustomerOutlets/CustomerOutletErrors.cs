using Crowbond.Common.Domain;

namespace Crowbond.Modules.CRM.Domain.CustomerOutlets;

public static class CustomerOutletErrors
{    
    public static Error NotFound(Guid customerOutletId) =>
    Error.NotFound("CustomerOutlet.NotFound", $"The customer outlet with the identifier {customerOutletId} was not found");
      
    public static Error RouteNotFound(Guid customerOutletRouteId) =>
    Error.NotFound("CustomerOutlet.RouteNotFound", $"The customer outlet route with the identifier {customerOutletRouteId} was not found");

    public static Error InvalidTimeFormat(string property) =>
    Error.Problem("CustomerOutlet.InvalidTimeFormat", $"Invalid time format in {property}. Expected format is HH:mm or HH:mm:ss.");

   
    public static readonly Error AlreadyActivated = Error.Problem(
    "CustomerOutlet.AlreadyActivated",
    "The outlet was already activated");

    public static readonly Error AlreadyDeactivated = Error.Problem(
    "CustomerOutlet.AlreadyDeactivated",
    "The outlet was already deactivated");

    public static readonly Error RouteHasConflict = Error.Conflict(
    "CustomerOutlet.RouteHasConflict",
    "The route has conflict");
}
