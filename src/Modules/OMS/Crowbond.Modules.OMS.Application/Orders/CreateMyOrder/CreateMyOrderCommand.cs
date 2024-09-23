using Crowbond.Common.Application.Messaging;
namespace Crowbond.Modules.OMS.Application.Orders.CreateMyOrder;

public sealed record CreateMyOrderCommand(
    Guid CustomerContactId,
    Guid CustomerOutletId,
    DateOnly ShippingDate,
    int DeliveryMethod,
    int PaymentMethod,
    string? CustomerComment) : ICommand;
