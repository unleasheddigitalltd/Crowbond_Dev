using Crowbond.Common.Domain;

namespace Crowbond.Modules.OMS.Domain.Drivers;

public sealed class Driver : Entity
{
    public Driver()
    {        
    }

    public Guid Id { get; private set; }

    public string FirstName { get; private set; }
    
    public string LastName { get; private set; }
    
    public string Username { get; private set; }
    
    public string Email { get; private set; }
    
    public string Mobile { get; private set; }

    public string? VehicleRegn { get; private set; }

    public static Driver Create(
        string firstName,
        string lastName,
        string username,
        string email,
        string mobile,
        string? vehicleRegn)
    {

        var driver = new Driver
        {
            Id = Guid.NewGuid(),
            FirstName = firstName,
            LastName = lastName,
            Username = username,
            Email = email,
            Mobile = mobile,
            VehicleRegn = vehicleRegn
        };

        driver.Raise(new DriverCreatedDomainEvent(driver.Id));

        return driver;
    }

    public void Update(
        string firstName,
        string lastName,
        string mobile,
        string? vehicleRegn)
    {
        FirstName = firstName;
        LastName = lastName;
        Mobile = mobile;
        VehicleRegn = vehicleRegn;
    }
}
