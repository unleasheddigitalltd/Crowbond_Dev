using Crowbond.Common.Domain;

namespace Crowbond.Modules.CRM.Domain.Products;

public static class ProductErrors
{    public static Error NotFound(Guid productId) =>
        Error.NotFound("Products.NotFound", $"The product with the identifier {productId} was not found");
    public static Error CategoryNotFound(Guid categoryId) =>
    Error.NotFound("Products.CategoryNotFound", $"Thecategory with the identifier {categoryId} was not found");

    public static Error BrandNotFound(Guid brandId) =>
    Error.NotFound("Products.BrandNotFound", $"The brand with the identifier {brandId} was not found");

    public static Error ProductGroupNotFound(Guid productGroupId) =>
    Error.NotFound("Products.ProductGroupNotFound", $"The product group with the identifier {productGroupId} was not found");
}
