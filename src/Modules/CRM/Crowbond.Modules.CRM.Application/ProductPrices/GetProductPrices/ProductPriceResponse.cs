namespace Crowbond.Modules.CRM.Application.ProductPrices.GetProductPrices;

public sealed record ProductPriceResponse(
    Guid Id,
    Guid ProductId,
    string ProductName,
    string ProductSku,
    string UnitOfMeasureName,
    string CategoryName,
    string BrandName,
    string ProductGroupName,
    Guid PriceTierId,
    decimal BasePurchasePrice,
    decimal SalePrice,
    DateOnly EffectiveDate);
