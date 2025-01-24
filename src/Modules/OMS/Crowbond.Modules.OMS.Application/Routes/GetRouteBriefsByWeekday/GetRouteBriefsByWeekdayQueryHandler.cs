using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Dapper;

namespace Crowbond.Modules.OMS.Application.Routes.GetRouteBriefsByWeekday;

internal sealed class GetRouteBriefsByWeekdayQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetRouteBriefsByWeekdayQuery, IReadOnlyCollection<RouteResponse>>
{
    public async Task<Result<IReadOnlyCollection<RouteResponse>>> Handle(GetRouteBriefsByWeekdayQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 id AS {nameof(RouteResponse.Id)},
                 name AS {nameof(RouteResponse.Name)},
                 position AS {nameof(RouteResponse.Position)}
             FROM oms.routes
             WHERE SUBSTRING(days_of_week, @Weekday, 1) = '1'
             """;

        List<RouteResponse> routes = (await connection.QueryAsync<RouteResponse>(sql, request)).AsList();

        return routes;
    }
}
