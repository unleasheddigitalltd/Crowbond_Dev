using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.Routes.GetRouteCustomerOutlets;

public sealed record GetRouteCustomerOutletsQuery(Guid RouteId) : IQuery<IReadOnlyCollection<CustomerOutletResponse>>;
