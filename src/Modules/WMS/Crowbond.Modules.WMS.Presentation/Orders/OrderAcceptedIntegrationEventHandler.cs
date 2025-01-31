using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Exceptions;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.IntegrationEvents;
using Crowbond.Modules.WMS.Application.Dispatches.AddDispatchLines;
using Crowbond.Modules.WMS.Application.Dispatches.CreateDispatch;
using MediatR;

namespace Crowbond.Modules.WMS.Presentation.Orders;

internal sealed class OrderAcceptedIntegrationEventHandler(ISender sender)
    : IntegrationEventHandler<OrderAcceptedIntegrationEvent>
{
    public override async Task Handle(
        OrderAcceptedIntegrationEvent integrationEvent,
        CancellationToken cancellationToken = default)
    {
        var request = integrationEvent.Lines.Select(l => new DispatchLineRequest(
                integrationEvent.OrderId,
                integrationEvent.OrderNo,
                integrationEvent.CustomerBusinessName,
                l.OrderLineId, 
                l.ProductId,
                l.Qty,
                l.IsBulk)).ToList();

        Result result = await sender.Send(
            new AddDispatchLinesCommand(integrationEvent.RouteTripId, request),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new CrowbondException(nameof(CreateDispatchCommand), result.Error);
        }
    }
}
