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

    public LocationType LocationType { get; private set; }

    public LocationLayer LocationLeyer { get; private set; }

    public LocationStatus Status { get; private set; }


    public static Location Create(
        Guid parentId,
        string name,
        LocationType locationType,
        LocationLayer locationLayer)
    {
        var location = new Location
        {
            Id = Guid.NewGuid(),
            ParentId = parentId,
            Name = name,
            LocationType = locationType,
            LocationLeyer = locationLayer,
            Status = LocationStatus.Active
        };

        return location;
    }
}
