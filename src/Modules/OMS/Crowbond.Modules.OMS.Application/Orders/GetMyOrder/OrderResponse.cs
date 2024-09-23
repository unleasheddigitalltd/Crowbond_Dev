using Crowbond.Modules.OMS.Domain.Orders;

namespace Crowbond.Modules.OMS.Application.Orders.GetMyOrder;

public sealed record OrderResponse(
    Guid Id,
    Guid CustomerId,
    string OrderNo,
    string CustomerAccountNumber,
    string CustomerBusinessName,
    DateOnly ShippingDate,
    OrderStatus Status,
    decimal DeliveryCharge,
    decimal OrderAmount);
