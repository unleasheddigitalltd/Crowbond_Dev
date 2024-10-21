using Crowbond.Common.Domain;

namespace Crowbond.Modules.OMS.Domain.RouteTripLogs;

public static class RouteTripLogErrors
{    
    public static Error NotFound(Guid routeTripLogId) =>
    Error.NotFound("RouteTripLog.NotFound", $"The route trip log with the identifier {routeTripLogId} was not found");

    public static Error NotFound(Guid routeTripId, Guid driverId) =>
    Error.NotFound("RouteTripLog.NotFound", $"The active route trip log for the route trip id {routeTripId}, and the driver id {driverId} was not found");

    public static Error Exists(Guid routeTripId) =>
    Error.Conflict("RouteTripLog.Exists", $"An active route trip log for route trip ID {routeTripId} already exists.");

    public static Error ActiveForDriverNotFound(Guid driverId) =>
    Error.Conflict("RouteTripLog.ActiveForDriverNotFound", $"An active route trip log for driver with identifier {driverId} was not found");

    public static readonly Error AlreadyLoggedOff = Error.Problem("RouteTripLog.LoggedOff", "The route trip log was already logged off");
}
