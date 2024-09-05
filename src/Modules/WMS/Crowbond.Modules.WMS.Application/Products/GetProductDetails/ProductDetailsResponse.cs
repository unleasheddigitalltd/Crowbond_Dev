using System.Xml.Linq;
using Crowbond.Modules.WMS.Domain.Products;

namespace Crowbond.Modules.WMS.Application.Products.GetProductDetails;

public sealed record ProductDetailsResponse()
{
    public Guid Id { get; }
    public string Sku { get; }
    public string Name { get; }
    public Guid? Parent { get; }
    public string FilterTypeName { get; }
    public string UnitOfMeasureName { get; }
    public Guid CategoryId { get; }
    public string CategoryName { get; }
    public string InventoryTypeName { get; }
    public int TaxRateType { get; }
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
    public bool IsActive { get; }
};
