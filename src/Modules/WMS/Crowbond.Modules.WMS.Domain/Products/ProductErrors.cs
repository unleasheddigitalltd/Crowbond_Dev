using Crowbond.Common.Domain;

namespace Crowbond.Modules.WMS.Domain.Products;
public static class ProductErrors
{
    public static Error NotFound(Guid productId) =>
    Error.NotFound("Products.NotFound", $"The product with the identifier {productId} was not found");

    public static Error FilterTypeNotFound(string filterTypeName) =>
    Error.NotFound("Products.FilterTypeNotFound", $"The filter type with the name {filterTypeName} was not found");

    public static Error InventoryTypeNotFound(string inventoryTypeName) =>
    Error.NotFound("Products.InventoryTypeNotFound", $"The inventory type with the name {inventoryTypeName} was not found");

    public static Error UnitOfMeasureNotFound(string unitOfMeasurName) =>
    Error.NotFound("Products.UnitOfMeasureNotFound", $"The unit of measure with the name {unitOfMeasurName} was not found");

    public static Error CategoryNotFound(Guid categoryId) =>
    Error.NotFound("Products.CategoryNotFound", $"Thecategory with the identifier {categoryId} was not found");
    
    public static Error BrandNotFound(Guid brandId) =>
    Error.NotFound("Products.BrandNotFound", $"The brand with the identifier {brandId} was not found");
    
    public static Error ProductGroupNotFound(Guid productGroupId) =>
    Error.NotFound("Products.ProductGroupNotFound", $"The product group with the identifier {productGroupId} was not found");


    public static readonly Error AlreadyHeld = Error.Problem(
        "Product.AlreadyHeld",
        "The product was already held");

    public static readonly Error AlreadyActived = Error.Problem(
        "Product.AlreadyActived",
        "The product was already actived");
}
