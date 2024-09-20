using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.Orders.GetOrders;
public sealed record GetOrdersQuery(string Search, string Sort, string Order, int Page, int Size) : IQuery<OrdersResponse>;
