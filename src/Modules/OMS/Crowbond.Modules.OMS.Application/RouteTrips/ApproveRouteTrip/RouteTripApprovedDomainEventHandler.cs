using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Exceptions;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Application.RouteTrips.GetRouteTrip;
using Crowbond.Modules.OMS.Domain.RouteTrips;
using Crowbond.Modules.OMS.IntegrationEvents;
using MediatR;

namespace Crowbond.Modules.OMS.Application.RouteTrips.ApproveRouteTrip;

internal sealed class RouteTripApprovedDomainEventHandler(ISender sender, IEventBus eventBus)
    : DomainEventHandler<RouteTripApprovedDomainEvent>
{
    public override async Task Handle(RouteTripApprovedDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        Result<RouteTripResponse> result = await sender.Send(new GetRouteTripQuery(domainEvent.RouteTripId), cancellationToken);

        if (result.IsFailure)
        {
            throw new CrowbondException(nameof(GetRouteTripQuery), result.Error);
        }

        await eventBus.PublishAsync(new RouteTripApprovedIntegrationEvent(
            domainEvent.Id,
            domainEvent.OccurredOnUtc,
            result.Value.Id,
            result.Value.Date,
            result.Value.RouteName),
            cancellationToken);
    }
}
