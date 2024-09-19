using Crowbond.Modules.OMS.Domain.PurchaseOrders;

namespace Crowbond.Modules.OMS.Application.Carts;

public sealed class CartItem
{
    public Guid ProductId { get; set; }
    public string ProductSku { get; set; }
    public string ProductName { get; set; }
    public string UnitOfMeasureName { get; set; }
    public decimal Qty { get; set; }
    public decimal UnitPrice { get; set; }
    public TaxRateType TaxRateType { get; set; }
}
