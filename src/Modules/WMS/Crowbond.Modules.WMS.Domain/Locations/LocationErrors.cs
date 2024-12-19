using Crowbond.Common.Domain;

namespace Crowbond.Modules.WMS.Domain.Locations;

public static class LocationErrors
{
    public static Error NotFound(Guid loactionId) =>
        Error.NotFound("Locations.NotFound", $"The location with the identifier {loactionId} was not found");

    public static Error ParentNotFound(Guid parentId) =>
        Error.NotFound("Locations.NotFound", $"The specified parent location with ID '{parentId}' does not exist.");
    
    public static Error HasChild(Guid loactionId) =>
        Error.NotFound("Locations.HasChild", $"The location with the identifier {loactionId} has child");
    
    public static Error IsAlreadyActive(Guid loactionId) =>
        Error.NotFound("Locations.IsAlreadyActive", $"The location with the identifier {loactionId} is already active");
    
    public static Error IsAlreadyHeld(Guid loactionId) =>
        Error.NotFound("Locations.IsAlreadyActive", $"The location with the identifier {loactionId} is already held");

    public static Error SacnCodeNotFound(string scanCode) =>
        Error.NotFound("Locations.SacnCodeNotFound", $"The location with the scan code {scanCode} was not found");

    public static Error NotTransfereUtility(Guid loactionId) =>
        Error.NotFound("Locations.NotTransfereUtility", $"The location with the identifier {loactionId} was not belong to a transfere utility");

    public static Error HasStock(Guid loactionId) =>
        Error.NotFound("Locations.HasStock", $"The location with the identifier {loactionId} has registred stock");

    public static readonly Error InvalidLocationTypeAssignment =
        Error.NotFound("Locations.InvalidLocationTypeAssignment", $"Only locations with a layer of \"Location\" can have a Location Type");

    public static readonly Error NeedParent =
    Error.NotFound("Locations.NeedParent", "The specified location layer must have a parent.");

    public static readonly Error CanNotHaveParent =
        Error.NotFound("Locations.CanNotHaveParent", "A location with the layer 'Site' cannot have a parent location");

    public static readonly Error InvalidParentLayerForArea =
        Error.NotFound("Locations.InvalidParentLayerForArea", "A location with the layer 'Area' must have a parent with the layer 'Site'.");
    
    public static readonly Error InvalidParentLayerForLocation =
        Error.NotFound("Locations.InvalidParentLayerForLocation", "A location with the layer 'Location' must have a parent with the layer 'Area'.");

}
