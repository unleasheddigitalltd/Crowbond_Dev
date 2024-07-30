using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.Routes.GetRoutes;

public sealed record GetRouteBriefsQuery() : IQuery<IReadOnlyCollection<RouteResponse>>;
