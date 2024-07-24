namespace Crowbond.Modules.OMS.Application.Orders.GetRouteOrders;
public sealed record OrderResponse
{
    public OrderResponse()
    {
        OrderLines = new List<OrderLineResponse>();
    }

    public Guid OrderHeaderId { get; }
    public string OrderNumber { get; }
    public string InvoiceNo { get; }
    public Guid CustomerId { get; }
    public string CustomerName { get; }
    public string CustomerMobile { get; }
    public string CustomerEmail { get; }
    public decimal OrderAmount { get; }
    public Guid ReouteId { get; }
    public string DeliveryAddressLine1 { get; }
    public string DeliveryAddressLine2 { get; }
    public string DeliveryAddressCountry { get; }
    public DateOnly DeliveryDate { get; }
    public string DeliveryAddressPostalCode { get; }
    public string DeliveryAddressTownCity { get; }
    public string DeliveryAddressCompany { get; }
    public List<OrderLineResponse> OrderLines { get; set; }
}
public sealed record OrderLineResponse(
        Guid OrderLineId,
        Guid OrderHeaderId,
        Guid ProductId,
        string ProductSku,
        string ProductName,
        decimal Qty);
