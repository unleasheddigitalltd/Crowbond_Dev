using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.RouteTrips.GetRouteTripsWithPendingOrders;

public sealed record GetRouteTripsWithPendingOrdersQuery() : IQuery<IReadOnlyCollection<RouteTripWithPendingOrdersResponse>>;
