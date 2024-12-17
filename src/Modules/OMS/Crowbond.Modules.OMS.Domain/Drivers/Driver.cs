using Crowbond.Common.Domain;

namespace Crowbond.Modules.OMS.Domain.Drivers;

public sealed class Driver : Entity
{
    private Driver()
    {        
    }

    public Guid Id { get; private set; }
    public bool IsActive { get; private set; }

    public static Driver Create(Guid userId)
    {

        var driver = new Driver
        {
            Id = userId,
            IsActive = true
        };

        return driver;
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }
}
