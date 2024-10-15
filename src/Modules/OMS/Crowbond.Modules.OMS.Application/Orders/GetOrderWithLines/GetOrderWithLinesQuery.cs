using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.Orders.GetOrderWithLines;

public sealed record GetOrderWithLinesQuery(Guid OrderHeaderId) : IQuery<OrderResponse>;
