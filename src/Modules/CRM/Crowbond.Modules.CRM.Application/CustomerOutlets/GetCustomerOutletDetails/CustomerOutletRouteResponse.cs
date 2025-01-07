using Crowbond.Modules.CRM.Domain.CustomerOutlets;

namespace Crowbond.Modules.CRM.Application.CustomerOutlets.GetCustomerOutletDetails;

public sealed record CustomerOutletRouteResponse(
    Guid CustomerOutletId,
    Guid RouteId,
    string RouteName,
    Weekday Weekday);
