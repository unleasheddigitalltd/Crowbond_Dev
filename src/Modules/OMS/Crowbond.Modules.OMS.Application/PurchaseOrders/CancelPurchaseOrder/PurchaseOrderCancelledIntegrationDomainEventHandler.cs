using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Exceptions;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Application.PurchaseOrders.GetPurchaseOrderHistory;
using Crowbond.Modules.OMS.Domain.PurchaseOrders;
using Crowbond.Modules.OMS.IntegrationEvents;
using MediatR;

namespace Crowbond.Modules.OMS.Application.PurchaseOrders.CancelPurchaseOrder;
internal sealed class PurchaseOrderCancelledIntegrationDomainEventHandler(ISender sender, IEventBus eventBus)
    : DomainEventHandler<PurchaseOrderCancelledDomainEvent>
{
    public override async Task Handle(
        PurchaseOrderCancelledDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        Result<IReadOnlyCollection<PurchaseOrderHistoryResponse>> result = await sender.Send(
            new GetPurchaseOrderHistoryQuery(domainEvent.PurchaseOrderHeaderId),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new CrowbondException(nameof(PurchaseOrderHistoryResponse), result.Error);
        }

        PurchaseOrderHistoryResponse? cancelledHistory = result.Value.FirstOrDefault(h => h.Status == (int)PurchaseOrderStatus.Cancelled);
        if (cancelledHistory is null)
        {
            throw new CrowbondException(PurchaseOrderErrors.NotDraft.Description);
        }

        await eventBus.PublishAsync(
            new PurchaseOrderCancelledIntegrationEvent(
                domainEvent.Id,
                domainEvent.OccurredOnUtc,
                cancelledHistory.PurchaseOrderHeaderId,
                cancelledHistory.ChangedBy,
                cancelledHistory.ChangedAt),
            cancellationToken);
    }
}
