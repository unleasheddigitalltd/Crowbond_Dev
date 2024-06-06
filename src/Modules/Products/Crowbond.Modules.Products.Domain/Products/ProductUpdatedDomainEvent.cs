using Crowbond.Common.Domain;

namespace Crowbond.Modules.Products.Domain.Products;

public sealed class ProductUpdatedDomainEvent(
    string Sku,
    string Name,
    Guid? ParentId,
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
    bool Active
    ) : DomainEvent
{
    public string Sku { get; init; } = Sku;
    public string Name { get; init; } = Name;
    public Guid? ParentId { get; init; } = ParentId;
    public string FilterTypeName { get; init; } = FilterTypeName;
    public string UnitOfMeasureName { get; init; } = UnitOfMeasureName;
    public Guid CategoryId { get; init; } = CategoryId;
    public string InventoryTypeName { get; init; } = InventoryTypeName;
    public int? Barcode { get; init; } = Barcode;
    public decimal? PackSize { get; init; } = PackSize;
    public string HandlingNotes { get; init; } = HandlingNotes;
    public bool QiCheck { get; init; } = QiCheck;
    public string Notes { get; init; } = Notes;
    public decimal? ReorderLevel { get; init; } = ReorderLevel;
    public decimal? Height { get; init; } = Height;
    public decimal? Width { get; init; } = Width;
    public decimal? Length { get; init; } = Length;
    public bool WeightInput { get; init; } = WeightInput;
    public bool Active { get; init; } = Active;
}
