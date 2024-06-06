namespace Crowbond.Modules.Products.Application.Products.CreateProduct.Dtos;

public sealed record ProductRequest(
    string Sku,
    string Name,
    Guid? Parent,
    string FilterType,
    string UnitOfMeasure,
    Guid Category,
    string InventoryType,
    int Barcode,
    int PackSize,
    string HandlingNotes,
    bool QiCheck,
    string Notes,
    int ReorderLevel,
    decimal? Height,
    decimal? Width,
    decimal? Length,
    bool WeightInput);
