using Crowbond.Common.Domain;

namespace Crowbond.Modules.OMS.Domain.RouteTripLogs;

public static class RouteTripLogErrors
{    
    public static Error NotFound(Guid routeTripLogId) =>
        Error.NotFound("RouteTripLog.NotFound", $"The route trip log with the identifier {routeTripLogId} was not found");
     
    public static Error NoActiveLog(Guid routeTripId) =>
        Error.NotFound("RouteTripLog.NoActiveLog", $"There are no active logs for the route trip with the identifier {routeTripId}");
        
    public static Error InvalidDriverLog(Guid routeTripId) =>
        Error.Problem("RouteTripLog.InvalidDriverLog", $"The current active log for route trip with the identifier {routeTripId} does not belong to this driver");
    
    public static Error AlreadyLoggedOff(Guid routeTripId) =>
        Error.Problem("RouteTripLog.AlreadyLoggedOff", $"The driver was already logged off from the route trip with identifier {routeTripId}");
        
    public readonly static Error AlreadyExists =
        Error.Conflict("RouteTripLog.AlreadyExists ", $"This driver already has an active log for this route trip");

    public static Error OtherActiveLogAlreadyExistsForDriver(Guid routeTripId) =>
        Error.Conflict("RouteTripLog.OtherActiveLogAlreadyExistsForDriver", $"This driver already has an active log for a different route trip with identifier {routeTripId}");
    
    public static Error OtherLogAlreadyExistsForRouteTrip(string driverName) =>
        Error.Conflict("RouteTripLog.OtherLogAlreadyExistsForRouteTrip", $"Driver {driverName} already has an active log for this route trip");
    
    public readonly static Error ActiveLogForDriverNotFound =
        Error.NotFound("RouteTripLog.ActiveLogForDriverNotFound", $"No active log entry was found for the specified driver");
    

}
