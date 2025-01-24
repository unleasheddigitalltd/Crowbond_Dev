using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Exceptions;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.IntegrationEvents;
using Crowbond.Modules.WMS.Application.Dispatches.CreateDispatch;
using MediatR;

namespace Crowbond.Modules.WMS.Presentation.RouteTrips;

internal sealed class RouteTripApprovedIntegrationEventHandler(ISender sender)
    : IntegrationEventHandler<RouteTripApprovedIntegrationEvent>
{
    public override async Task Handle(
        RouteTripApprovedIntegrationEvent integrationEvent, 
        CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(
            new CreateDispatchCommand(
                integrationEvent.RouteTripId,
                integrationEvent.RouteTripDate,
                integrationEvent.RouteName),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new CrowbondException(nameof(CreateDispatchCommand), result.Error);
        }
    }
}
