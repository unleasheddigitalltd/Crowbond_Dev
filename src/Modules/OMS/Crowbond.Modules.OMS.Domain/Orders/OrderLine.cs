using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Domain.Products;

namespace Crowbond.Modules.OMS.Domain.Orders;

public sealed class OrderLine: Entity
{

    private OrderLine()
    {
    }

    public Guid Id { get; private set; }

    public Guid OrderHeaderId { get; private set; }

    public Guid ProductId { get; private set; }

    public string ProductSku { get; private set; }

    public string ProductName { get; private set; }

    public string UnitOfMeasureName { get; private set; }

    public Guid CategoryId { get; private set; }

    public string CategoryName { get; private set; }

    public Guid BrandId { get; private set; }

    public string BrandName { get; private set; }

    public Guid ProductGroupId { get; private set; }

    public string ProductGroupName { get; private set; }

    public decimal UnitPrice { get; private set; }

    public decimal OrderedQty { get; private set; }

    public decimal? ActualQty { get; private set; }

    public decimal? DeliveredQty { get; private set; }

    public decimal SubTotal { get; private set; }

    public decimal? DeductionSubTotal { get; private set; }

    public decimal Tax { get; private set; }

    public decimal? DeductionTax { get; private set; }

    public decimal LineTotal { get; private set; }

    public decimal? DeductionLineTotal { get; private set; }

    public TaxRateType TaxRateType { get; private set; } 

    public OrderLineStatus Status { get; private set; }

    public Guid? RejectReasonId { get; private set; }

    public string? DeliveryComments { get; private set; }


    public OrderHeader Header { get; }

    internal static OrderLine Create(
        Guid productId,
        string productSku,
        string productName,
        string unitOfMeasureName,
        Guid categoryId,
        string categoryName,
        Guid brandId,
        string brandName,
        Guid productGroupId,
        string productGroupName,
        decimal unitPrice,
        decimal orderedQty,
        TaxRateType taxRateType)
    {
        var orderLine = new OrderLine
        {
            Id = Guid.NewGuid(),
            ProductId = productId,
            ProductSku = productSku,
            ProductName = productName,
            UnitOfMeasureName = unitOfMeasureName,
            CategoryId = categoryId,
            CategoryName = categoryName,
            BrandId = brandId,
            BrandName = brandName,
            ProductGroupId = productGroupId,
            ProductGroupName = productGroupName,
            UnitPrice = unitPrice,
            TaxRateType = taxRateType,
            OrderedQty = orderedQty,
            Status = OrderLineStatus.Pending
        };

        orderLine.SubTotal = orderLine.UnitPrice * orderLine.OrderedQty;
        orderLine.Tax = orderLine.SubTotal * orderLine.GetTaxRate(orderLine.TaxRateType);
        orderLine.LineTotal = orderLine.SubTotal + orderLine.Tax;

        return orderLine;
    }

    internal void UpdateOrderedQty(decimal orderedQty)
    {
        OrderedQty = orderedQty;
        SubTotal = UnitPrice * OrderedQty;
        Tax = SubTotal * GetTaxRate(TaxRateType);
        LineTotal = SubTotal + Tax;
    }

    internal void UpdateActualQty(decimal actualQty)
    {
        ActualQty = actualQty;
        SubTotal = UnitPrice * actualQty;
        Tax = SubTotal * GetTaxRate(TaxRateType);
        LineTotal = SubTotal + Tax;
    }
    
    internal void Deliver(decimal deliveredQty, Guid? rejectReasonId, string? deliveryComments)
    {
        if (Status != OrderLineStatus.Pending)
        {
            return;
        }

        DeliveredQty = deliveredQty;
        DeductionSubTotal = UnitPrice * (ActualQty - DeliveredQty);
        DeductionTax = DeductionSubTotal * GetTaxRate(TaxRateType);
        DeductionLineTotal = DeductionSubTotal + DeductionTax;

        RejectReasonId = rejectReasonId;
        DeliveryComments = deliveryComments;

        Status = deliveredQty == 0 ? OrderLineStatus.Returned : OrderLineStatus.Delivered;
    }

    private decimal GetTaxRate(TaxRateType taxRateType)
    {
        return taxRateType switch
        {
            TaxRateType.NoVat => 0,
            TaxRateType.Vat => 0.2m,
            _ => 0
        };
    }
}
