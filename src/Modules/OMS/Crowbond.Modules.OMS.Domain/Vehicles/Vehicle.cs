using Crowbond.Common.Domain;

namespace Crowbond.Modules.OMS.Domain.Vehicles;

public sealed class Vehicle: Entity
{
    private Vehicle()
    {        
    }

    public Guid Id { get; private set; }

    public string VehicleRegn { get; private set; }

    public static Vehicle Create(string vehicleRegn)
    {
        var vehicle = new Vehicle
        {
            Id = Guid.NewGuid(),
            VehicleRegn = vehicleRegn
        };

        return vehicle;
    }

    public void Update(string vehicleRegn)
    {
        VehicleRegn = vehicleRegn;
    }
}
