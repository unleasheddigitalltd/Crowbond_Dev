namespace Crowbond.Modules.OMS.Application.RouteTrips.GetRouteTrip;

public sealed record RouteTripResponse(Guid Id, DateOnly Date, string RouteName);
