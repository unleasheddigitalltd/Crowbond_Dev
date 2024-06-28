namespace Crowbond.Modules.WMS.Application.Stocks.GetStockDetails;

public sealed record StockDetailsResponse(
    Guid Id,
    decimal OriginalQty,
    decimal CurrentQty,
    Guid Location);
