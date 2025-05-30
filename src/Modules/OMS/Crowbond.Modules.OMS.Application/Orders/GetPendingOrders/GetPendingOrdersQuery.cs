using Crowbond.Common.Application.Messaging;
using Crowbond.Modules.OMS.Application.Orders.GetOrders;

namespace Crowbond.Modules.OMS.Application.Orders.GetPendingOrders;

public sealed record GetPendingOrdersQuery(string Search, string Sort, string Order, int Page, int Size) : IQuery<OrdersResponse>;
