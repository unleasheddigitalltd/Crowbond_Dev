using Crowbond.Common.Application.Messaging;
using Crowbond.Modules.OMS.Domain.RouteTrips;

namespace Crowbond.Modules.OMS.Application.RouteTrips.GetRouteTripsByStatus;

public sealed record GetRouteTripsByStatusQuery(RouteTripStatus Status) : IQuery<IReadOnlyCollection<RouteTripByStatusResponse>>;
