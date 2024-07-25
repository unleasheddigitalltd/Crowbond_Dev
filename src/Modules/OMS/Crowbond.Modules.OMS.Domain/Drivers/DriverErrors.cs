using Crowbond.Common.Domain;

namespace Crowbond.Modules.OMS.Domain.Drivers;

public static class DriverErrors
{    
    public static Error NotFound(Guid driverId) =>
    Error.NotFound("Driver.NotFound", $"The driver with the identifier {driverId} was not found");

    public static Error VehicleRegnNotFound() =>
        Error.NotFound("Driver.VehicleRegnNotFound", $"The vehicle registration no for this driver was not found");
}
