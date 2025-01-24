using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.RouteTrips.GetRouteTrip;

public sealed record GetRouteTripQuery(Guid RouteTripId): IQuery<RouteTripResponse>;
