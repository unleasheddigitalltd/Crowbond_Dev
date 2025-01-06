using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Domain.Routes;
using Dapper;

namespace Crowbond.Modules.OMS.Application.Routes.GetRoute;

internal sealed class GetRouteQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetRouteQuery, RouteResponse>
{
    public async Task<Result<RouteResponse>> Handle(GetRouteQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        string sql =
            $"""
             SELECT 
                 id AS {nameof(Route.Id)},
                 name AS {nameof(Route.Name)},
                 position AS {nameof(Route.Position)},
                 cut_off_time AS {nameof(Route.CutOffTime)},
                 days_of_week AS {nameof(Route.DaysOfWeek)}
             FROM oms.routes
             WHERE id = @RouteId
             """;

        RouteResponse? route = await connection.QuerySingleOrDefaultAsync<RouteResponse>(sql, request);

        if (route is null)
        {
            return Result.Failure<RouteResponse>(RouteErrors.NotFound(request.RouteId));
        }

        return route;
    }
}
