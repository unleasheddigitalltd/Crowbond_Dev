using Crowbond.Modules.CRM.Domain.CustomerOutlets;

namespace Crowbond.Modules.CRM.Application.Routes.GetRouteCustomerOutlets;

public sealed record CustomerOutletRouteResponse(
    Guid CustomerOutletId,
    Guid RouteId,
    string RouteName,
    Weekday Weekday);
