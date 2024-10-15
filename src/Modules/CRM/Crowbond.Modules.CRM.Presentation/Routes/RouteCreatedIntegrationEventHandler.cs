using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Exceptions;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.Routes.CreateRoute;
using Crowbond.Modules.OMS.IntegrationEvents;
using MediatR;

namespace Crowbond.Modules.CRM.Presentation.Routes;

internal sealed class RouteCreatedIntegrationEventHandler(ISender sender)
    : IntegrationEventHandler<RouteCreatedIntegrationEvent>
{
    public override async Task Handle(
        RouteCreatedIntegrationEvent integrationEvent,
        CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(
            new CreateRouteCommand(
                integrationEvent.RouteId,
                integrationEvent.Name,
                integrationEvent.Position,
                integrationEvent.CutOffTime,
                integrationEvent.DaysOfWeek),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new CrowbondException(nameof(CreateRouteCommand), result.Error);
        }
    }
}
