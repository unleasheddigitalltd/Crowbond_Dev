namespace Crowbond.Modules.CRM.Application.SupplierProducts.GetSupplierProducts;

public sealed record SupplierProductResponse(
    Guid Id,
    Guid SupplierId,
    Guid ProductId,
    string ProductName,
    string ProductSku,
    string UnitOfMeasureName,
    string CategoryName,
    string BrandName,
    string ProductGroupName,
    decimal UnitPrice,
    int TaxRateType,
    bool IsDefault,
    string? Comments);
