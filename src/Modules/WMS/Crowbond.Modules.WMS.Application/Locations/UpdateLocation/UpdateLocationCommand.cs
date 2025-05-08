using Crowbond.Common.Application.Messaging;
using Crowbond.Modules.WMS.Domain.Locations;

namespace Crowbond.Modules.WMS.Application.Locations.UpdateLocation;

public sealed record UpdateLocationCommand(
    Guid LocationId,
    Guid? ParentId,
    string Name,
    string? ScanCode,
    string? NetworkAddress,
    string? PrinterName,
    LocationType? LocationType,
    LocationLayer LocationLayer) : ICommand;
