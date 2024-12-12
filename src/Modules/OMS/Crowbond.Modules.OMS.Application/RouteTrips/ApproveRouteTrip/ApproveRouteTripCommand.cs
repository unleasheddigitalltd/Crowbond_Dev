using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.RouteTrips.ApproveRouteTrip;

public sealed record ApproveRouteTripCommand(Guid RouteTripId) : ICommand;
