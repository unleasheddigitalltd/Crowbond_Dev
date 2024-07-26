using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.RouteTrips.CreateRouteTrip;

public sealed record CreateRouteTripCommand(RouteTripRequest RouteTrip) : ICommand<Guid>;
