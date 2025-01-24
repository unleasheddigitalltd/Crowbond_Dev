using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Exceptions;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.IntegrationEvents;
using Crowbond.Modules.WMS.Application.Receipts.CancelReceipt;
using MediatR;

namespace Crowbond.Modules.WMS.Presentation.PurchaseOrders;
internal sealed class PurchaseOrderCancelledIntegrationEventHandler(ISender sender)
    : IntegrationEventHandler<PurchaseOrderCancelledIntegrationEvent>
{
    public override async Task Handle(
        PurchaseOrderCancelledIntegrationEvent integrationEvent,
        CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(
            new CancelReceiptCommand(integrationEvent.PurchaseOrderId),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new CrowbondException(nameof(CancelReceiptCommand), result.Error);
        }
    }
}
