namespace Crowbond.Modules.CRM.Application.CustomerProducts.GetCustomerProducts;
public sealed record ProductResponse(
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
