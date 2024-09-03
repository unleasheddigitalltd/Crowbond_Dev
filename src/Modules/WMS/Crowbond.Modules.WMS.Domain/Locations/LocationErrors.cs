using Crowbond.Common.Domain;

namespace Crowbond.Modules.WMS.Domain.Locations;

public static class LocationErrors
{
    public static Error NotFound(Guid loactionId) =>
        Error.NotFound("Locations.NotFound", $"The location with the identifier {loactionId} was not found");
}
