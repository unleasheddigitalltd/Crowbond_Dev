using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Exceptions;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.Routes.UpdateRoute;
using Crowbond.Modules.OMS.IntegrationEvents;
using MediatR;

namespace Crowbond.Modules.CRM.Presentation.Routes;

internal sealed class RouteUpdatedIntegrationEventHandler(ISender sender)
    : IntegrationEventHandler<RouteUpdatedIntegrationEvent>
{
    public override async Task Handle(
        RouteUpdatedIntegrationEvent integrationEvent,
        CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(
            new UpdateRouteCommand(
                integrationEvent.RouteId,
                integrationEvent.Name,
                integrationEvent.Position,
                integrationEvent.CutOffTime,
                integrationEvent.DaysOfWeek),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new CrowbondException(nameof(UpdateRouteCommand), result.Error);
        }
    }
}
