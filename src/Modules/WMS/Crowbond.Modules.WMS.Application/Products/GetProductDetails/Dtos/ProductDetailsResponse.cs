using System.Xml.Linq;

namespace Crowbond.Modules.WMS.Application.Products.GetProductDetails.Dtos;

public sealed record ProductDetailsResponse()
{
    public Guid Id { get; }
    public string Sku { get; }
    public string Name { get; }
    public Guid? Parent { get; }
    public string FilterType { get; }
    public string UnitOfMeasure { get; }
    public Guid Category { get; }
    public string InventoryType { get; }
    public int? Barcode { get; }
    public decimal? PackSize { get; }
    public string HandlingNotes { get; }
    public bool QiCheck { get; }
    public string Notes { get; }
    public decimal? ReorderLevel { get; }
    public decimal? Height { get; }
    public decimal? Width { get; }
    public decimal? Length { get; }
    public bool WeightInput { get; }
    public bool Active { get; }
};
