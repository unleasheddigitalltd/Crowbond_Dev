using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Domain.Orders;
using Dapper;

namespace Crowbond.Modules.OMS.Application.Vehicles.GetVehicle;

internal sealed class GetVehicleQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetVehicleQuery, VehicleResponse>
{
    public async Task<Result<VehicleResponse>> Handle(GetVehicleQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 v.id AS {nameof(VehicleResponse.Id)},
                 v.vehicle_regn AS {nameof(VehicleResponse.VehicleRegn)},
                 (r.logged_off_time is null AND r.logged_on_time is not null) AS {nameof(VehicleResponse.InUse)}
             FROM oms.vehicles v
             LEFT JOIN oms.route_trip_logs r ON r.vehicle_id = v.id
             WHERE v.id = @VehicleId
             """;

        VehicleResponse? vehicle = await connection.QuerySingleOrDefaultAsync<VehicleResponse>(sql, request);

        if (vehicle is null)
        {
            return Result.Failure<VehicleResponse>(OrderErrors.NotFound(request.VehicleId));
        }

        return vehicle;
    }
}
