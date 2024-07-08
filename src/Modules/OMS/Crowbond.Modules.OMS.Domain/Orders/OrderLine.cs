using Crowbond.Common.Domain;

namespace Crowbond.Modules.OMS.Domain.Orders;

public sealed class OrderLine
{
    public OrderLine()
    {
    }

    public Guid Id { get; private set; }

    public Guid OrderId { get; private set; }

    public Guid ProductId { get; private set; }

    public string ProductSku { get; private set; }

    public string ProductName { get; private set; }

    public decimal UnitPrice { get; private set; }

    public decimal Qty { get; private set; }

    public decimal SubTotal { get; private set; }

    public decimal Tax { get; private set; }

    public decimal LineTotal { get; private set; }

    public bool FOC { get; private set; }

    public bool Taxable { get; private set; }


    public static Result<OrderLine> Create(
        Guid orderId,
        Guid productId,
        string productSku,
        string productName,
        decimal unitPrice,
        int qty,
        decimal subTotal,
        decimal tax,
        decimal lineTotal,
        bool foc,
        bool taxable)
    {
        var orderLine = new OrderLine
        {
            Id = Guid.NewGuid(),
            OrderId = orderId,
            ProductId = productId,
            ProductSku = productSku,
            ProductName = productName,
            UnitPrice = unitPrice,
            Qty = qty,
            SubTotal = subTotal,
            Tax = tax,
            LineTotal = lineTotal,
            FOC = foc,
            Taxable = taxable            
        };

        return orderLine;
    }
}
