namespace Crowbond.Modules.OMS.Application.Carts;

public sealed class CartItem
{
    public Guid ProductId { get; set; }

    public decimal Quantity { get; set; }

    public decimal Price { get; set; }
}
