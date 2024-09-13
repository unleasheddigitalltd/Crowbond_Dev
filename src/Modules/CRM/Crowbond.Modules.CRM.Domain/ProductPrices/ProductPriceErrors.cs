using Crowbond.Common.Domain;

namespace Crowbond.Modules.CRM.Domain.ProductPrices;

public static class ProductPriceErrors
{
    public static Error NotFound(Guid productPriceId) =>
        Error.NotFound("ProductPrices.NotFound", $"The product price with the identifier {productPriceId} was not found");
    
    public static Error HasDuplicatedProduct(Guid productId) =>
        Error.NotFound("ProductPrices.HasDuplicatedProduct", $"The product with the identifier {productId} is duplicated");
    
    public static Error HasDuplicatedPriceTier(Guid priceTierId) =>
        Error.NotFound("ProductPrices.HasDuplicatedPriceTier", $"The price-tier with the identifier {priceTierId} is duplicated");


}
