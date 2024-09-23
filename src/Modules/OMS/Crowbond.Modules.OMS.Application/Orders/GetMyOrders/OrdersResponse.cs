using Crowbond.Common.Application.Pagination;
using Crowbond.Modules.OMS.Domain.Orders;

namespace Crowbond.Modules.OMS.Application.Orders.GetMyOrders;

public sealed class OrdersResponse : PaginatedResponse<Order>
{
    public OrdersResponse(IReadOnlyCollection<Order> orders, IPagination pagination)
        : base(orders, pagination)
    { }
}

public sealed record Order(
    Guid Id,
    Guid CustomerId,
    string OrderNo,
    string CustomerAccountNumber,
    string CustomerBusinessName,
    DateOnly ShippingDate,
    OrderStatus Status,
    decimal DeliveryCharge,
    decimal OrderAmount);
