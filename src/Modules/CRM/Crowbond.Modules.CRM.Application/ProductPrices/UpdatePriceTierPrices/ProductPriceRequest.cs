namespace Crowbond.Modules.CRM.Application.ProductPrices.UpdatePriceTierPrices;
public sealed record ProductPriceRequest(Guid ProductId, decimal BasePurchasePrice, decimal SalePrice);
