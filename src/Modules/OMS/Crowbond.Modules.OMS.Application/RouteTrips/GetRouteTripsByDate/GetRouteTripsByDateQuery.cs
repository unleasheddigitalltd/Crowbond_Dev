using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.RouteTrips.GetRouteTripsByDate;

public sealed record GetRouteTripsByDateQuery(DateOnly Date) : IQuery<IReadOnlyCollection<RouteTripResponse>>;
