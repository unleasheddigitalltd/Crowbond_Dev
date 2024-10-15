using Crowbond.Common.Domain;

namespace Crowbond.Modules.OMS.Domain.Routes;

public sealed class RouteCreatedDomainEvent(
    Guid routeId,
    string name,
    int position,
    TimeOnly cutOffTime,
    string daysOfWeek)
    : DomainEvent
{
    public Guid RouteId { get; init; } = routeId;
    public string Name { get; init; } = name;
    public int Position { get; init; } = position;
    public TimeOnly CutOffTime { get; init; } = cutOffTime;
    public string DaysOfWeek { get; init; } = daysOfWeek;
}
