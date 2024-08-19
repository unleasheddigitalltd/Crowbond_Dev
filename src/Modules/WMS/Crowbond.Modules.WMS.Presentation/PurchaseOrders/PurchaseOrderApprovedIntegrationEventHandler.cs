using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Exceptions;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.IntegrationEvents;
using Crowbond.Modules.WMS.Application.Receipts.CreateReceipt;
using MediatR;

namespace Crowbond.Modules.WMS.Presentation.PurchaseOrders;
internal sealed class PurchaseOrderApprovedIntegrationEventHandler(ISender sender)
    : IntegrationEventHandler<PurchaseOrderApprovedIntegrationEvent>
{
    public override async Task Handle(
        PurchaseOrderApprovedIntegrationEvent integrationEvent,
        CancellationToken cancellationToken = default)
    {
        var request = new ReceiptRequest(
            integrationEvent.UtcNow,
            integrationEvent.PurchaseOrderId,
            integrationEvent.PurchaseOrderNo,
            string.Empty,
            integrationEvent.ReceiptLines.Select(l => new ReceiptRequest.ReceiptLineRequest(
                l.ProductId, l.Qty, l.UnitPrice, null, null, string.Empty)).ToList());

        Result result = await sender.Send(
            new CreateReceiptCommand(
                request),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new CrowbondException(nameof(CreateReceiptCommand), result.Error);
        }
    }
}
