using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Dapper;

namespace Crowbond.Modules.OMS.Application.Routes.GetRouteBriefs;

internal sealed class GetRouteBriefsQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetRouteBriefsQuery, IReadOnlyCollection<RouteResponse>>
{
    public async Task<Result<IReadOnlyCollection<RouteResponse>>> Handle(GetRouteBriefsQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 id AS {nameof(RouteResponse.Id)},
                 name AS {nameof(RouteResponse.Name)},
                 position AS {nameof(RouteResponse.Position)}
             FROM oms.routes
             """;

        List<RouteResponse> routes = (await connection.QueryAsync<RouteResponse>(sql, request)).AsList();

        return routes;
    }
}
