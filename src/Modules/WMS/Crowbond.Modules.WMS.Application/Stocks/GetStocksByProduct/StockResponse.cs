namespace Crowbond.Modules.WMS.Application.Stocks.GetStocksByProduct;

public sealed record StockResponse(
    Guid Id,
    string Sku,
    string Name,
    string CategoryName,
    string Batch,
    string UnitOfMeasureName,
    decimal InStock,
    string Location,
    int DaysInStock,
    bool IsActive,
    string Status);
