using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.Routes.GetRouteBriefs;

public sealed record GetRouteBriefsQuery() : IQuery<IReadOnlyCollection<RouteResponse>>;
