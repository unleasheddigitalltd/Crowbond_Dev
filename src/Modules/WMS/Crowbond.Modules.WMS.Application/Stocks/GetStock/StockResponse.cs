namespace Crowbond.Modules.WMS.Application.Stocks.GetStock;

public sealed record StockResponse(
    Guid Id,
    string Sku,
    string Name,
    string Category,
    string Batch,
    string UnitOfMeasure,
    decimal InStock,
    decimal Available,
    decimal Allocated,
    decimal OnHold,
    string Location,
    decimal ReorderLevel,
    int DaysInStock,
    DateTime SellByDate,
    DateTime UseByDate,
    bool Active);
