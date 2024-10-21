using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Messaging;
using Crowbond.Modules.OMS.Domain.Routes;
using Crowbond.Modules.OMS.IntegrationEvents;

namespace Crowbond.Modules.OMS.Application.Routes.CreateRoute;

internal sealed class RouteCreatedIntegrationDomainEventHandler(IEventBus eventBus)
    : DomainEventHandler<RouteCreatedDomainEvent>
{
    public override async Task Handle(
        RouteCreatedDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        await eventBus.PublishAsync(
            new RouteCreatedIntegrationEvent(
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
