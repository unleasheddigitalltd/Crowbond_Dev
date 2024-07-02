namespace Crowbond.Modules.WMS.Application.Stocks.GetStockDetails;

public sealed record StockDetailsResponse(
    Guid Id,
    string UnitOfMeasure,
    decimal OriginalQty,
    decimal CurrentQty,
    decimal Available,
    decimal Allocated,
    decimal OnHold,
    decimal ReorderLevel,
    Guid Location,
    DateTime ReceivedDate,
    DateTime SellByDate,
    DateTime UseByDate,
    string? Note,
    string Status);
