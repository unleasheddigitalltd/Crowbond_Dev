using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Messaging;
using Crowbond.Modules.WMS.Domain.Stocks;
using Crowbond.Modules.WMS.IntegrationEvents;

namespace Crowbond.Modules.WMS.Application.Stocks;

internal sealed class StockTransactionGeneratedDomainEventHandler(IEventBus eventBus)
    : DomainEventHandler<StockTransactionGeneratedDomainEvent>
{
    public override async Task Handle(
        StockTransactionGeneratedDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        await eventBus.PublishAsync(
            new StockTransactionGeneratedIntegrationEvent(
                domainEvent.Id,
                domainEvent.OccurredOnUtc,
                domainEvent.StockId,
                domainEvent.Quantity,
                domainEvent.PosAdjustment),
            cancellationToken);
    }
}
