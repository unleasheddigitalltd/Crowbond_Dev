using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.Orders.CreateOrder;

public sealed record CreateOrderCommand(
    Guid CustomerId,
    Guid CustomerOutletId,
    DateOnly ShippingDate,
    int DeliveryMethod,
    int PaymentMethod,
    string? CustomerComment) : ICommand<Guid>;
