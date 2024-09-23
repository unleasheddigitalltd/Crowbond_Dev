namespace Crowbond.Modules.OMS.Application.Orders.CreateOrder;

public sealed record OrderRequest(
    Guid CustomerId,
    Guid CustomerOutletId,
    DateOnly ShippingDate,
    int DeliveryMethod,
    int PaymentMethod,
    string? CustomerComment,
    List<OrderLineRequest> Lines);
