namespace Crowbond.Modules.CRM.Application.ProductPrices.UpdateProductPrices;

public sealed record ProductPriceRequest(Guid PriceTierId, decimal BasePurchasePrice, decimal SalePrice);
