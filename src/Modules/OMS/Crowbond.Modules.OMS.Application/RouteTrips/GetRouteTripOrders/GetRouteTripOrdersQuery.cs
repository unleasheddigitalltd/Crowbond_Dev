using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.RouteTrips.GetRouteTripOrders;
public sealed record GetRouteTripOrdersQuery(Guid RouteTripId) : IQuery<IReadOnlyCollection<OrderResponse>>;
