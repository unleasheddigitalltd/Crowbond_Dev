using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.Ticketing.Application.Orders.GetOrder;

public sealed record GetOrderQuery(Guid OrderId) : IQuery<OrderResponse>;
