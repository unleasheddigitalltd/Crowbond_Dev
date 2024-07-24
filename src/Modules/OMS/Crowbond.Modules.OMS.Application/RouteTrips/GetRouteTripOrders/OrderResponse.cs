using Crowbond.Modules.OMS.Domain.Payments;

namespace Crowbond.Modules.OMS.Application.RouteTrips.GetRouteTripOrders;
public sealed record OrderResponse
{
    public OrderResponse()
    {
        OrderLines = new List<OrderLineResponse>();
    }

    public Guid OrderHeaderId { get; }
    public string OrderNumber { get; }
    public Guid CustomerId { get; }
    public string CustomerBusinessName { get; }
    public string DeliveryLocationName { get; }
    public string DeliveryFullName { get; }
    public string DeliveryPhone { get; }
    public string DeliveryMobile { get; }
    public string DeliveryNotes { get; }
    public string DeliveryAddressLine1 { get; }
    public string DeliveryAddressLine2 { get; }
    public string DeliveryAddressTownCity { get; }
    public string DeliveryAddressCounty { get; }
    public string DeliveryAddressPostalCode { get; }
    public DateOnly ShippingDate { get; }
    public Guid RouteTripId { get; }
    public string RouteName { get; }
    public decimal OrderAmount { get; }
    public PaymentMethod PaymentMethod { get; }
    public string CustomerComment { get; }
    public List<OrderLineResponse> OrderLines { get; set; }
}
public sealed record OrderLineResponse(
        Guid OrderLineId,
        Guid OrderHeaderId,
        Guid ProductId,
        string ProductSku,
        string ProductName,
        decimal Qty);
