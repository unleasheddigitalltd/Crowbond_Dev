using Crowbond.Common.Domain;

namespace Crowbond.Modules.WMS.Domain.Locations;
public sealed class LocationType : Entity
{
    public static readonly LocationType Site = new("Site");
    public static readonly LocationType Area = new("Area");
    public static readonly LocationType Location = new("Location");

    public LocationType(string name)
    {
        Name = name;
    }

    public LocationType()
    {
    }

    public string Name { get; private set; }
}
