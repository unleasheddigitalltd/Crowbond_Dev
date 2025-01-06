using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.Routes.GetRoute;

public sealed record GetRouteQuery(Guid RouteId) : IQuery<RouteResponse>;
