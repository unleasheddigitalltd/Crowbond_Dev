using Crowbond.Common.Domain;

namespace Crowbond.Modules.WMS.Domain.Locations;

public sealed class Location : Entity
{
    public Location()
    {
    }

    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public LocationStatus Status { get; private set; }


}
