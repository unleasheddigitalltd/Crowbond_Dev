using Crowbond.Common.Application.Messaging;
using Crowbond.Modules.OMS.Domain.Routes;

namespace Crowbond.Modules.OMS.Application.Routes.GetRouteBriefsByWeekday;

public sealed record GetRouteBriefsByWeekdayQuery(Weekday Weekday) : IQuery<IReadOnlyCollection<RouteResponse>>;
