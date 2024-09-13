using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Exceptions;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Application.PurchaseOrders.GetPurchaseOrderWithLines;
using Crowbond.Modules.OMS.Domain.PurchaseOrders;
using Crowbond.Modules.OMS.IntegrationEvents;
using MediatR;

namespace Crowbond.Modules.OMS.Application.PurchaseOrders.ApprovePurchaseOrder;
internal sealed class PurchaseOrderApprovedIntegrationDomainEventHandler(ISender sender, IEventBus eventBus)
    : DomainEventHandler<PurchaseOrderApprovedDomainEvent>
{
    public override async Task Handle(
        PurchaseOrderApprovedDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        Result<PurchaseOrderResponse> result = await sender.Send(
            new GetPurchaseOrderWithLinesQuery(domainEvent.PurchaseOrderId),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new CrowbondException(nameof(GetPurchaseOrderWithLinesQuery), result.Error);
        }

        await eventBus.PublishAsync(
            new PurchaseOrderApprovedIntegrationEvent(
                domainEvent.Id,
                domainEvent.OccurredOnUtc,
                result.Value.Id,
                result.Value.PurchaseOrderNo,
                result.Value.CreatedBy,
                result.Value.CreatedOnUtc,
                result.Value.Lines.Select(l => new PurchaseOrderApprovedIntegrationEvent.ReceiptLine(l.ProductId, l.Qty, l.UnitPrice)).ToList()),
            cancellationToken);
    }
}
