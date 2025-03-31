using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Exceptions;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.IntegrationEvents;
using Crowbond.Modules.WMS.Application.Dispatches.AddDispatchLines;
using Crowbond.Modules.WMS.Application.Dispatches.CreateDispatch;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Crowbond.Modules.WMS.Presentation.Orders;

internal sealed class OrderAcceptedIntegrationEventHandler(ISender sender, ILogger<OrderAcceptedIntegrationEventHandler> logger)
    : IntegrationEventHandler<OrderAcceptedIntegrationEvent>
{
    public override async Task Handle(
        OrderAcceptedIntegrationEvent integrationEvent,
        CancellationToken cancellationToken = default)
    {
        logger.LogInformation(
            "Processing accepted order {OrderId} for customer {CustomerName}. Creating {LineCount} dispatch lines for route trip {RouteTripId}",
            integrationEvent.OrderId,
            integrationEvent.CustomerBusinessName,
            integrationEvent.Lines.Count,
            integrationEvent.RouteTripId);

        try
        {
            var request = integrationEvent.Lines.Select(l => {
                logger.LogDebug(
                    "Creating dispatch line request for order line {OrderLineId}, product {ProductId}, quantity {Quantity}, isBulk: {IsBulk}",
                    l.OrderLineId,
                    l.ProductId,
                    l.Qty,
                    l.IsBulk);

                return new DispatchLineRequest(
                    integrationEvent.OrderId,
                    integrationEvent.OrderNo,
                    integrationEvent.CustomerBusinessName,
                    l.OrderLineId, 
                    l.ProductId,
                    l.Qty,
                    l.IsBulk);
            }).ToList();

            logger.LogInformation(
                "Sending command to add {LineCount} dispatch lines for order {OrderId}",
                request.Count,
                integrationEvent.OrderId);

            Result result = await sender.Send(
                new AddDispatchLinesCommand(integrationEvent.RouteTripId, request),
                cancellationToken);

            if (result.IsFailure)
            {
                logger.LogError(
                    "Failed to create dispatch lines for order {OrderId}. Error: {Error}",
                    integrationEvent.OrderId,
                    result.Error);

                throw new CrowbondException(nameof(CreateDispatchCommand), result.Error);
            }

            logger.LogInformation(
                "Successfully created dispatch lines for order {OrderId}. Picking tasks will be generated.",
                integrationEvent.OrderId);
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "Unexpected error while processing accepted order {OrderId}",
                integrationEvent.OrderId);
           
        }
    }
}
