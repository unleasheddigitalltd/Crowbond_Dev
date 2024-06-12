namespace Crowbond.Modules.WMS.Application.Products.GetProduct.Dtos;

public sealed record ProductResponse(
    Guid Id,
    string Sku,
    string Name,
    Guid? Parent,
    string FilterType,
    string UnitOfMeasure,
    Guid Category,
    string InventoryType,
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
    bool Active
    );
