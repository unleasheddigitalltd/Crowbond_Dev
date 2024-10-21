namespace Crowbond.Modules.CRM.Application.CustomerProducts.GetCustomerProduct;
public sealed record CustomerProductResponse(
    Guid Id,
    Guid CustomerId,
    Guid ProductId,
    string ProductName,
    string ProductSku,
    string UnitOfMeasureName,
    string CategoryName,
    string BrandName,
    string ProductGroupName,
    decimal? FixedPrice,
    decimal? FixedDiscount,
    string? Comments,
    DateOnly EffectiveDate,
    DateOnly? ExpiryDate);
