using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Domain.Drivers;
using Dapper;

namespace Crowbond.Modules.OMS.Application.Drivers.GetDriverRouteTripAvtivation;

internal sealed class GetDriverRouteTripAvtivationQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetDriverRouteTripAvtivationQuery, ActiveRouteTripResponse>
{
    public async Task<Result<ActiveRouteTripResponse>> Handle(GetDriverRouteTripAvtivationQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 d.id AS {nameof(ActiveRouteTripResponse.DriverId)},
                 l.id AS {nameof(ActiveRouteTripResponse.ActiveRouteTripId)},
                 r.name AS {nameof(ActiveRouteTripResponse.ActiveRouteName)}
             FROM oms.drivers d
             LEFT JOIN oms.route_trip_logs l 
                 ON d.id = l.driver_id AND l.logged_off_time IS NULL
             LEFT JOIN oms.route_trips rt 
                 ON rt.id = l.route_trip_id
             LEFT JOIN oms.routes r 
                 ON r.id = rt.route_id
             WHERE d.id = @DriverId;
             """;

        ActiveRouteTripResponse? driverActivation = await connection.QuerySingleOrDefaultAsync<ActiveRouteTripResponse>(sql, request);

        if (driverActivation is null)
        {
            return Result.Failure<ActiveRouteTripResponse>(DriverErrors.NotFound(request.DriverId));
        }

        return driverActivation;
    }
}
