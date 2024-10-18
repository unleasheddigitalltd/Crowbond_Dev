namespace Crowbond.Modules.WMS.Application.Tasks.Picking.GetProductSuggestedStock;

public sealed record StockResponse(
    Guid Id,
    Guid ProductId,
    Guid LocationId,
    string LocationName,
    decimal CurrentQty,
    string BatchNumber,
    DateOnly ReceivedDate,
    DateOnly SellByDate,
    DateOnly UseByDate,
    string Note);
