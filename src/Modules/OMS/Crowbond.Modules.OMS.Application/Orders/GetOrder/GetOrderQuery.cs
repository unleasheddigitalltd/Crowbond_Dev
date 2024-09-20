using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.Orders.GetOrder;

public sealed record GetOrderQuery(Guid OrderHeaderId) : IQuery<OrderResponse>;
