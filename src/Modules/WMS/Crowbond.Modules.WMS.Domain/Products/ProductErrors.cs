﻿using Crowbond.Common.Domain;

namespace Crowbond.Modules.WMS.Domain.Products;
public static class ProductErrors
{
    public static Error NotFound(Guid productId) =>
    Error.NotFound("Products.NotFound", $"The product with the identifier {productId} was not found");

    public static Error FilterTypeNotFound(string filterTypeName) =>
    Error.NotFound("FilterType.NotFound", $"The filter type with the name {filterTypeName} was not found");

    public static Error InventoryTypeNotFound(string inventoryTypeName
        ) =>
    Error.NotFound("InventoryType.NotFound", $"The inventory type with the name {inventoryTypeName} was not found");

    public static Error UnitOfMeasureNotFound(string unitOfMeasurName) =>
    Error.NotFound("UnitOfMeasure.NotFound", $"The unit of measure with the name {unitOfMeasurName} was not found");

    public static readonly Error AlreadyHeld = Error.Problem(
        "Product.AlreadyHeld",
        "The product was already held");

    public static readonly Error AlreadyActived = Error.Problem(
        "Product.AlreadyActived",
        "The product was already actived");
}
