namespace Crowbond.Modules.WMS.Application.Products.CreateProduct;

public sealed record ProductResponse(
    Guid Id,
    string Sku,
    string Name,
    Guid? Parent,
    string FilterTypeName,
    string UnitOfMeasureName,
    Guid CategoryId,
    string InventoryTypeName,
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
    bool IsActive
    );
