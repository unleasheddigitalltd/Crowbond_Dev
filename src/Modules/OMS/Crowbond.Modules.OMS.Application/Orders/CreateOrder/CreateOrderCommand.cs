using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.Orders.CreateOrder;

public sealed record CreateOrderCommand(OrderRequest Order) : ICommand<Guid>;
