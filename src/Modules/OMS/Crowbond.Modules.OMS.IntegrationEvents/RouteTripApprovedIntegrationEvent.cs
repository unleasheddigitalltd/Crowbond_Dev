using Crowbond.Common.Application.EventBus;

namespace Crowbond.Modules.OMS.IntegrationEvents;

public sealed class RouteTripApprovedIntegrationEvent : IntegrationEvent
{
    public RouteTripApprovedIntegrationEvent(
        Guid id,
        DateTime occurredOnUtc,
        Guid routeTripId,
        DateOnly routeTripDate,
        string routeName)
        : base(id, occurredOnUtc)
    {
        RouteTripId = routeTripId;
        RouteName = routeName;
        RouteTripDate = routeTripDate;
    }

    public Guid RouteTripId { get; set; }
    public DateOnly RouteTripDate { get; set; }
    public string RouteName { get; set; }
}
