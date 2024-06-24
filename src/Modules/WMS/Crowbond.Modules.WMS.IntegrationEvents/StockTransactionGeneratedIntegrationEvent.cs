using Crowbond.Common.Application.EventBus;

namespace Crowbond.Modules.WMS.IntegrationEvents;

public sealed class StockTransactionGeneratedIntegrationEvent : IntegrationEvent
{
    public StockTransactionGeneratedIntegrationEvent(Guid id, DateTime occurredOnUtc, Guid stockId, decimal quantity, bool posAdjustment) 
        : base(id, occurredOnUtc)
    {
        StockId = stockId;
        Quantity = quantity;
        PosAdjustment = posAdjustment;
    }

    public Guid StockId { get; init; }

    public decimal Quantity { get; init; }

    public bool PosAdjustment { get; init; }
}
