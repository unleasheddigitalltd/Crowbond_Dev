namespace Crowbond.Modules.CRM.Application.CustomerProducts.GetCustomerProductPrice;
public sealed record CustomerProductPriceResponse(
    Guid Id,
    Guid CustomerId,
    Guid ProductId,
    string ProductName,
    string ProductSku,
    string UnitOfMeasureName,
    Guid CategoryId,
    string CategoryName,
    Guid BrandId,
    string BrandName,
    Guid ProductGroupId,
    string ProductGroupName,
    decimal UnitPrice,
    int TaxRateType);
