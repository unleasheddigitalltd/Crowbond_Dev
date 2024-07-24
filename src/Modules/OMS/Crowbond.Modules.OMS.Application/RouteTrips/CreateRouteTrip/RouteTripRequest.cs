namespace Crowbond.Modules.OMS.Application.RouteTrips.CreateRouteTrip;

public sealed record RouteTripRequest(DateOnly Date, Guid RouteId, string Comments);
