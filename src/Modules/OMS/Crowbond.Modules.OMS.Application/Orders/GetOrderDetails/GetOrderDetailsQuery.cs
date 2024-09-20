using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.Orders.GetOrderDetails;

public sealed record GetOrderDetailsQuery(Guid OrderHeaderId) : IQuery<OrderResponse>;
