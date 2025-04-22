namespace Crowbond.Modules.CRM.Application.SupplierProducts.GetSupplierProductsWithStock;

public sealed record ProductWithStockResponse(
    Guid Id,
    string ProductName,
    string ProductSku,
    string UnitOfMeasureName,
    string CategoryName,
    string BrandName,
    string ProductGroupName,
    decimal UnitPrice,
    int TaxRateType,
    decimal StockLevel
);
