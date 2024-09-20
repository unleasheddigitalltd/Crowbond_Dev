using Crowbond.Modules.OMS.Domain.Orders;

namespace Crowbond.Modules.OMS.Application.Orders.GetOrder;

public sealed record OrderResponse(
    Guid Id,
    Guid CustomerId,
    Guid CustomerBusinessName,
    DateOnly ShippingDate,
    OrderStatus Status,
    decimal DeliveryCharge,
    decimal TotalPrice)
{
    public List<OrderItemResponse> OrderItems { get; } = [];
}
