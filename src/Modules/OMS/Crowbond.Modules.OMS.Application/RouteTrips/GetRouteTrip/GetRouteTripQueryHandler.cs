using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Domain.Orders;
using Dapper;

namespace Crowbond.Modules.OMS.Application.RouteTrips.GetRouteTrip;

internal sealed class GetRouteTripQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetRouteTripQuery, RouteTripResponse>
{
    public async Task<Result<RouteTripResponse>> Handle(GetRouteTripQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 t.id AS {nameof(RouteTripResponse.Id)},
                 t.date AS {nameof(RouteTripResponse.Date)},
                 r.name AS {nameof(RouteTripResponse.RouteName)}
             FROM oms.route_trips t
             INNER JOIN oms.routes r ON r.id = t.route_id
             WHERE t.id = @RouteTripId
             """;

        RouteTripResponse? routeTrip = await connection.QuerySingleOrDefaultAsync<RouteTripResponse>(sql, request);

        if (routeTrip is null)
        {
            return Result.Failure<RouteTripResponse>(OrderErrors.NotFound(request.RouteTripId));
        }

        return routeTrip;
    }
}
