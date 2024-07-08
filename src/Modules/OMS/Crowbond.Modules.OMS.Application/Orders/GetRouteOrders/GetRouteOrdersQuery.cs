using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.Orders.GetRouteOrders;
public sealed record GetRouteOrdersQuery(Guid RouteId) : IQuery<IReadOnlyCollection<OrderResponse>>;
