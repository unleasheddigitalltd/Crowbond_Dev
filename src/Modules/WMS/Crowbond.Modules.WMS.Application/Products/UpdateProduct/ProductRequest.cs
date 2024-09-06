using Crowbond.Modules.WMS.Domain.Products;

namespace Crowbond.Modules.WMS.Application.Products.UpdateProduct;
public sealed record ProductRequest(
    Guid? Id,
    string Sku,
    string Name,
    Guid? Parent,
    string FilterTypeName,
    string UnitOfMeasureName,
    Guid CategoryId,
    string InventoryTypeName,
    TaxRateType TaxRateType,
    int? Barcode,
    decimal? PackSize,
    string HandlingNotes,
    bool QiCheck,
    string Notes,
    decimal? ReorderLevel,
    decimal? Height,
    decimal? Width,
    decimal? Length,
    bool WeightInput,
    bool IsActive);
