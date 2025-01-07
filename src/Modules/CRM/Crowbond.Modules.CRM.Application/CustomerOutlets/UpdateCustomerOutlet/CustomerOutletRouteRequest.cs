using Crowbond.Modules.CRM.Domain.CustomerOutlets;

namespace Crowbond.Modules.CRM.Application.CustomerOutlets.UpdateCustomerOutlet;

public sealed record CustomerOutletRouteRequest(Guid RouteId, Weekday Weekday);
