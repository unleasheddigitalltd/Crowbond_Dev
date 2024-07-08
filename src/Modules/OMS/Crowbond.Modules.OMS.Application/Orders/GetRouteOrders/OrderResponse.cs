namespace Crowbond.Modules.OMS.Application.Orders.GetRouteOrders;
public sealed record OrderResponse
{
    public OrderResponse()
    {
        OrderLines = new List<OrderLineResponse>();
    }

    public Guid OrderHeaderId { get; }
    public string InvoiceNo { get; }
    public Guid CustomerId { get; }
    public decimal OrderAmount { get; }
    public string CustomerName { get; }
    public string CustomerMobile { get; }
    public string CustomerEmail { get; }
    public Guid ReouteId { get; }
    public string ShippingAddressLine1 { get; }
    public string ShippingAddressLine2 { get; }
    public string ShippingAddressCountry { get; }
    public DateOnly ShippingDate { get; }
    public string ShippingAddressPostalCode { get; }
    public string ShippingAddressTownCity { get; }
    public string SalesOrderNumber { get; }
    public string ShippingAddressCompany { get; }
    public List<OrderLineResponse> OrderLines { get; set; }
}
public sealed record OrderLineResponse(
        Guid OrderLineId,
        Guid OrderHeaderId,
        Guid ProductId,
        string ProductSku,
        string ProductName,
        decimal Qty);
