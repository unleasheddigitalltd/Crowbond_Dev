using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Exceptions;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.IntegrationEvents;
using Crowbond.Modules.WMS.Application.Receipts.CreateReceiptWithLines;
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
            PurchaseOrderId: integrationEvent.PurchaseOrderId,
            PurchaseOrderNo: integrationEvent.PurchaseOrderNo,
            ReceiptLines: integrationEvent.ReceiptLines.Select(l => new ReceiptRequest.ReceiptLineRequest(
                l.ProductId, l.Qty, l.UnitPrice)).ToList());

        Result result = await sender.Send(
            new CreateReceiptWithLinesCommand(request),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new CrowbondException(nameof(CreateReceiptWithLinesCommand), result.Error);
        }
    }
}
