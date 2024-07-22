using Crowbond.Common.Domain;

namespace Crowbond.Modules.CRM.Domain.Products;

public static class ProductErrors
{
    public static Error NotFound(Guid productId) =>
    Error.NotFound("Product.NotFound", $"The product with the identifier {productId} was not found");
}
