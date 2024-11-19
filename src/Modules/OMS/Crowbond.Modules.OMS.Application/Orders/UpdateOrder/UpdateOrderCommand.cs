using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.Orders.UpdateOrder;

public sealed record UpdateOrderCommand(
    Guid OrderId,
    Guid CustomerOutletId,
    DateOnly ShippingDate,
    int DeliveryMethod,
    int PaymentMethod,
    string? CustomerComment) : ICommand;
