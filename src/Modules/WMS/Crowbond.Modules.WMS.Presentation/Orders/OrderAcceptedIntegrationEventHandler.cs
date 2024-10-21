using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Exceptions;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.IntegrationEvents;
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
        var request = new DispatchRequest(
            OrderId: integrationEvent.OrderId,
            OrderNo: integrationEvent.OrderNo)
        {
            Lines = integrationEvent.Lines.Select(l => new DispatchLineRequest(
                l.ProductId, l.Qty)).ToList()
        };

        Result result = await sender.Send(
            new CreateDispatchCommand(request),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new CrowbondException(nameof(CreateDispatchCommand), result.Error);
        }
    }
}
