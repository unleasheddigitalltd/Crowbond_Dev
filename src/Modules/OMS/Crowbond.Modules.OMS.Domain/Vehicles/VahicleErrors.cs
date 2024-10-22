using Crowbond.Common.Domain;

namespace Crowbond.Modules.OMS.Domain.Vehicles;

public static class VahicleErrors
{
    public static Error NotFound(Guid vehicleId) =>
    Error.NotFound("Vehicles.NotFound", $"The vehicle with the identifier {vehicleId} was not found");
}
