using Crowbond.Common.Domain;

namespace Crowbond.Modules.OMS.Domain.RouteTripLogs;

public sealed class RouteTripLog : Entity
{
    public RouteTripLog()
    {        
    }

    public Guid Id { get; private set; }

    public Guid RouteTripId { get; private set; }

    public Guid DriverId { get; private set; }

    public string VehicleRegn { get; private set; }

    public DateTime LoggedOnTime { get; private set; }

    public DateTime? LoggedOffTime { get; private set; }

    public decimal? Temperature { get; private set; }

    public static RouteTripLog Create(Guid routeTripId, Guid driverId, string vehicleRegn, DateTime loggedOnTime)
    {
        var routeTripLog = new RouteTripLog
        {
            Id = Guid.NewGuid(),
            RouteTripId = routeTripId,
            DriverId = driverId,
            VehicleRegn = vehicleRegn,
            LoggedOnTime = loggedOnTime
        };

        return routeTripLog;
    }


}
