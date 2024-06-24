using Crowbond.Common.Domain;

namespace Crowbond.Modules.WMS.Domain.Stocks;
public sealed class StockTransactionGeneratedDomainEvent(Guid stockId, decimal quantity, bool posAdjustment) : DomainEvent
{
    public Guid StockId { get; init; } = stockId;

    public decimal Quantity { get; init; } = quantity;

    public bool PosAdjustment { get; init; } = posAdjustment;
}
