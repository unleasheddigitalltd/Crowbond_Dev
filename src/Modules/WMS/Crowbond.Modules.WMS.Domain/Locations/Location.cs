using Crowbond.Common.Domain;

namespace Crowbond.Modules.WMS.Domain.Locations;

public sealed class Location : Entity
{
    private Location()
    {
    }

    public Guid Id { get; private set; }

    public Guid? ParentId { get; private set; }

    public string Name { get; private set; }

    public string? ScanCode { get; private set; }

    public LocationType? LocationType { get; private set; }

    public LocationLayer LocationLayer { get; private set; }

    public LocationStatus Status { get; private set; }


    public static Location Create(
        Guid parentId,
        string name,
        string? scanCode,
        LocationType? locationType,
        LocationLayer locationLayer)
    {
        var location = new Location
        {
            Id = Guid.NewGuid(),
            ParentId = parentId,
            Name = name,
            ScanCode = scanCode,
            LocationType = locationType,
            LocationLayer = locationLayer,
            Status = LocationStatus.Active
        };

        return location;
    }
}
