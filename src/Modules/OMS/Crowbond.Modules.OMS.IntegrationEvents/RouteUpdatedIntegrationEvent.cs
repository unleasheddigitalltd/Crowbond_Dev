using Crowbond.Common.Application.EventBus;

namespace Crowbond.Modules.OMS.IntegrationEvents;

public sealed class RouteUpdatedIntegrationEvent
     : IntegrationEvent
{
    public RouteUpdatedIntegrationEvent(
        Guid id,
        DateTime occurredOnUtc,
        Guid routeId,
        string name,
        int position,
        TimeOnly cutOffTime,
        string daysOfWeek)
        : base(id, occurredOnUtc)
    {
        RouteId = routeId;
        Name = name;
        Position = position;
        CutOffTime = cutOffTime;
        DaysOfWeek = daysOfWeek;
    }

    public Guid RouteId { get; init; }

    public string Name { get; init; }

    public int Position { get; init; }

    public TimeOnly CutOffTime { get; init; }

    public string DaysOfWeek { get; init; }
}
