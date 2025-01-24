using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.Routes.GetRoutes;

public sealed record GetRoutesQuery(string Search, string Sort, string Order, int Page, int Size) : IQuery<RoutesResponse>;
