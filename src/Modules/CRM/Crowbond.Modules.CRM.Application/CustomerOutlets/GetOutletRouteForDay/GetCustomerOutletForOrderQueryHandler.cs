using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.CustomerOutlets.GetOutletRouteForDay;
using Crowbond.Modules.CRM.Domain.CustomerOutlets;
using Dapper;

namespace Crowbond.Modules.CRM.Application.CustomerOutlets.GetOutletRouteForDay;

internal sealed class GetOutletRouteForDayQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetOutletRouteForDayQuery, OutletRouteForDayResponse>
{
    public async Task<Result<OutletRouteForDayResponse>> Handle(GetOutletRouteForDayQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 cor.route_id AS {nameof(OutletRouteForDayResponse.routeId)},
                 co.location_name as {nameof(OutletRouteForDayResponse.routeName)}
             FROM crm.customer_outlet_routes cor
             LEFT JOIN crm.customer_outlets co ON cor.customer_outlet_id = co.id
             WHERE cor.customer_outlet_id = @CustomerOutletId AND cor.weekday = @Day
             """;

        var customerOutlet = await connection.QuerySingleOrDefaultAsync<OutletRouteForDayResponse>(sql, request);

        return customerOutlet ?? Result.Failure<OutletRouteForDayResponse>(CustomerOutletErrors.RouteNotFound(request.CustomerOutletId));
    }
}
