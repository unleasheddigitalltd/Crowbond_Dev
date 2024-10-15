using Crowbond.Modules.OMS.Domain.Products;

namespace Crowbond.Modules.OMS.Application.Carts;

public sealed class CartItem
{
    public Guid ProductId { get; set; }
    public string ProductSku { get; set; }
    public string ProductName { get; set; }
    public string UnitOfMeasureName { get; set; }
    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; }
    public Guid BrandId { get; set; }
    public string BrandName { get; set; }
    public Guid ProductGroupId { get; set; }
    public string ProductGroupName { get; set; }
    public decimal Qty { get; set; }
    public decimal UnitPrice { get; set; }
    public TaxRateType TaxRateType { get; set; }
}
