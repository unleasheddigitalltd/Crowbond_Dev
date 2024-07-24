namespace Crowbond.Modules.OMS.Application.RouteTrips.GetRouteTripsByDate;

public sealed record RouteTripResponse(Guid Id, string RouteName, int Position, int Status, string Comments);
