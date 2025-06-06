﻿using Crowbond.Modules.WMS.Domain.Locations;

namespace Crowbond.Modules.WMS.Application.Locations.GetLocation;

public sealed record LocationResponse(
    Guid Id,
    Guid? ParentId,
    string Name,
    string? ScanCode,
    string? NetworkAddress,
    string? PrinterName,
    LocationType? LocationType,
    LocationLayer LocationLayer,
    LocationStatus Status);
