using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.RouteTrips.LogOffRouteTrip;

public sealed record LogOffRouteTripCommand(Guid RouteTripId, Guid DriverId) : ICommand;
