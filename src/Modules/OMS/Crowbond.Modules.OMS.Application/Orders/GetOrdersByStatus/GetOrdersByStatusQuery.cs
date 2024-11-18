using Crowbond.Common.Application.Messaging;
using Crowbond.Modules.OMS.Domain.Orders;

namespace Crowbond.Modules.OMS.Application.Orders.GetOrdersByStatus;

public sealed record GetOrdersByStatusQuery(OrderStatus OrderStatus, string Search, string Sort, string Order, int Page, int Size) : IQuery<OrdersResponse>;
