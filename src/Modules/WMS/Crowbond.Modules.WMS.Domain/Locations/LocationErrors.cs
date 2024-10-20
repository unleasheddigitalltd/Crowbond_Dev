using Crowbond.Common.Domain;

namespace Crowbond.Modules.WMS.Domain.Locations;

public static class LocationErrors
{
    public static Error NotFound(Guid loactionId) =>
        Error.NotFound("Locations.NotFound", $"The location with the identifier {loactionId} was not found");

    public static Error NotTransfereUtility(Guid loactionId) =>
        Error.NotFound("Locations.NotTransfereUtility", $"The location with the identifier {loactionId} was not belong to a transfere utility");
}
