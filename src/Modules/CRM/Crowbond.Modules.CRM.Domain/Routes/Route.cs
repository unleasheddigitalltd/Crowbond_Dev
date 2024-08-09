using Crowbond.Common.Domain;

namespace Crowbond.Modules.CRM.Domain.Routes;

public sealed class Route : Entity
{
    private Route()
    {        
    }

    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public int Position { get; private set; }

    public TimeOnly CutOffTime { get; private set; }

    public bool IsActive { get; private set; }

    public string DaysOfWeek { get; private set; }

    public Route Create(
        string name,
        int position,
        TimeOnly cutOffTime,
        string DaysOfWeek)
    {
        var route = new Route
        {
            Id = Guid.NewGuid(),
            Name = name,
            Position = position,
            CutOffTime = cutOffTime,
            DaysOfWeek = DaysOfWeek
        };

        return route;
    }

    public void Update(
        string name,
        int position,
        TimeOnly cutOffTime,
        string daysOfWeek)
    {
        Name = name;
        Position = position;
        CutOffTime = cutOffTime;
        DaysOfWeek = daysOfWeek;
    }
}
