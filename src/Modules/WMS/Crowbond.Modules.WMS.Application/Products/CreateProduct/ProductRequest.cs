using Crowbond.Modules.WMS.Domain.Products;

namespace Crowbond.Modules.WMS.Application.Products.CreateProduct;

public sealed record ProductRequest(
    string Sku,
    string Name,
    Guid? Parent,
    string FilterTypeName,
    string UnitOfMeasureName,
    Guid CategoryId,
    Guid DefaultLocation,
    Guid BrandId,
    Guid ProductGroupId,
    string InventoryTypeName,
    TaxRateType TaxRateType,
    int? Barcode,
    decimal? PackSize,
    string? HandlingNotes,
    bool QiCheck,
    string? Notes,
    decimal? ReorderLevel,
    decimal? Height,
    decimal? Width,
    decimal? Length,
    bool WeightInput);
