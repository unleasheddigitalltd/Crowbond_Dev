using Crowbond.Common.Domain;

namespace Crowbond.Modules.OMS.Domain.RouteTripLogs;

public sealed class RouteTripLog : Entity
{
    private RouteTripLog()
    {        
    }

    public Guid Id { get; private set; }

    public Guid RouteTripId { get; private set; }

    public Guid DriverId { get; private set; }

    public Guid? VehicleId { get; private set; }

    public DateTime LoggedOnTime { get; private set; }

    public DateTime? LoggedOffTime { get; private set; }

    public decimal? Temperature { get; private set; }

    public bool ComplianceCompleted { get; private set; }

    public static RouteTripLog Create(Guid routeTripId, Guid driverId, DateTime loggedOnTime)
    {
        var routeTripLog = new RouteTripLog
        {
            Id = Guid.NewGuid(),
            RouteTripId = routeTripId,
            DriverId = driverId,
            LoggedOnTime = loggedOnTime
        };

        return routeTripLog;
    }

    public Result LogOff(DateTime loggedOffTime)
    {
        if (LoggedOffTime != null)
        {
            return Result.Failure(RouteTripLogErrors.AlreadyLoggedOff(RouteTripId));
        }

        LoggedOffTime = loggedOffTime;

        return Result.Success();
    }

    public Result AssignVehicle(Guid vehicleId)
    {
        if (LoggedOffTime != null)
        {
            return Result.Failure(RouteTripLogErrors.AlreadyLoggedOff(RouteTripId));
        }

        VehicleId = vehicleId;

        return Result.Success();
    }
}
