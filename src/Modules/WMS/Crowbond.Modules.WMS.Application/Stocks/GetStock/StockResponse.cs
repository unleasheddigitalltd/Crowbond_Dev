namespace Crowbond.Modules.WMS.Application.Stocks.GetStock;

public sealed record StockResponse(
    Guid Id,
    string Sku,
    string Name,
    string Category,
    string Batch,
    string UnitOfMeasure,
    decimal InStock,
    string Location,
    int DaysInStock,
    bool Active);

