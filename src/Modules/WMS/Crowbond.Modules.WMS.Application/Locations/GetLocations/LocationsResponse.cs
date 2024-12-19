using Crowbond.Common.Application.Pagination;
using Crowbond.Modules.WMS.Domain.Locations;

namespace Crowbond.Modules.WMS.Application.Locations.GetLocations;

public sealed class LocationsResponse : PaginatedResponse<Location>
{
    public LocationsResponse(IReadOnlyCollection<Location> locations, IPagination pagination)
        : base(locations, pagination)
    { }
}

public sealed record Location(
    Guid Id,
    Guid? ParentId,
    string Name,
    string? ScanCode,
    LocationType? LocationType,
    LocationLayer LocationLayer,
    LocationStatus Status);
