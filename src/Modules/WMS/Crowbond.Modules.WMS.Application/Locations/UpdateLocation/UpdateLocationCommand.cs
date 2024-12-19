using Crowbond.Common.Application.Messaging;
using Crowbond.Modules.WMS.Domain.Locations;

namespace Crowbond.Modules.WMS.Application.Locations.UpdateLocation;

public sealed record UpdateLocationCommand(
    Guid LocationId,
    Guid? ParentId,
    string Name,
    string? ScanCode,
    LocationType? LocationType,
    LocationLayer LocationLayer) : ICommand;
