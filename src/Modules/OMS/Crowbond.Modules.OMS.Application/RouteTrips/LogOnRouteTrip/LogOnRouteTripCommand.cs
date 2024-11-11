using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.RouteTrips.LogOnRouteTrip;

public sealed record LogOnRouteTripCommand(Guid RouteTripId, Guid DriverId) : ICommand;
