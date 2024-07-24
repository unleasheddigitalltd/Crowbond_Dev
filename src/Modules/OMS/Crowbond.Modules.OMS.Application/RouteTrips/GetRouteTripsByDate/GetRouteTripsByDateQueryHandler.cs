using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Dapper;

namespace Crowbond.Modules.OMS.Application.RouteTrips.GetRouteTripsByDate;

internal sealed class GetRouteTripsByDateQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetRouteTripsByDateQuery, IReadOnlyCollection<RouteTripResponse>>
{
    public async Task<Result<IReadOnlyCollection<RouteTripResponse>>> Handle(GetRouteTripsByDateQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 t.id AS {nameof(RouteTripResponse.Id)},
                 r.name AS {nameof(RouteTripResponse.RouteName)},
                 r.position AS {nameof(RouteTripResponse.Position)},
                 t.status AS {nameof(RouteTripResponse.Status)},
                 t.comments AS {nameof(RouteTripResponse.Comments)}
             FROM oms.route_trips t
             INNER JOIN oms.routes r ON r.id = t.route_id
             WHERE t.date = @Date
             """;

        List<RouteTripResponse> routeTrips = (await connection.QueryAsync<RouteTripResponse>(sql, request)).AsList();

        return routeTrips;
    }
}
