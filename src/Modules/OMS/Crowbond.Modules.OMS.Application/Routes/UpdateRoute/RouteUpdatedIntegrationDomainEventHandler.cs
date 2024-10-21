using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Messaging;
using Crowbond.Modules.OMS.Domain.Routes;
using Crowbond.Modules.OMS.IntegrationEvents;

namespace Crowbond.Modules.OMS.Application.Routes.UpdateRoute;

internal sealed class RouteUpdatedIntegrationDomainEventHandler(IEventBus eventBus)
    : DomainEventHandler<RouteUpdatedDomainEvent>
{
    public override async Task Handle(
        RouteUpdatedDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        await eventBus.PublishAsync(
            new RouteUpdatedIntegrationEvent(
                domainEvent.Id,
                domainEvent.OccurredOnUtc,
                domainEvent.RouteId,
                domainEvent.Name,
                domainEvent.Position,
                domainEvent.CutOffTime,
                domainEvent.DaysOfWeek),
            cancellationToken);
    }
}
