using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Application.Pagination;
using Crowbond.Common.Domain;
using Dapper;

namespace Crowbond.Modules.OMS.Application.Vehicles.GetVehicles;

internal sealed class GetVehiclesQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetVehiclesQuery, VehiclesResponse>
{
    public async Task<Result<VehiclesResponse>> Handle(GetVehiclesQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        string sortOrder = request.Order.Equals("ASC", StringComparison.OrdinalIgnoreCase) ? "ASC" : "DESC";
        string orderByClause = request.Sort switch
        {
            "id" => "id",
            "vehicleRegn" => "vehicle_regn",
            _ => "vehicle_regn" // Default sorting
        };

        string sql = $@"
            WITH FilteredVehicles AS (
                SELECT
                    v.id AS {nameof(Vehicle.Id)},
                    v.vehicle_regn AS {nameof(Vehicle.VehicleRegn)},
                    (r.logged_off_time is null AND r.logged_on_time is not null) AS {nameof(Vehicle.InUse)},
                    ROW_NUMBER() OVER (ORDER BY {orderByClause} {sortOrder}) AS RowNum
                FROM oms.vehicles v
                LEFT JOIN oms.route_trip_logs r ON r.vehicle_id = v.id
                WHERE
                    v.vehicle_regn ILIKE '%' || @Search || '%'
            )
            SELECT 
                {nameof(Vehicle.Id)},
                {nameof(Vehicle.VehicleRegn)},
                {nameof(Vehicle.InUse)}
            FROM FilteredVehicles
            WHERE RowNum BETWEEN ((@Page) * @Size) + 1 AND (@Page + 1) * @Size            
            ORDER BY RowNum;

            SELECT Count(*) 
                FROM oms.vehicles v
                LEFT JOIN oms.route_trip_logs r ON r.vehicle_id = v.id
                WHERE
                    v.vehicle_regn ILIKE '%' || @Search || '%'
        ";

        SqlMapper.GridReader multi = await connection.QueryMultipleAsync(sql, request);

        var vehicles = (await multi.ReadAsync<Vehicle>()).ToList();
        int totalCount = await multi.ReadSingleAsync<int>();

        int totalPages = (int)Math.Ceiling(totalCount / (double)request.Size);
        int currentPage = request.Page;
        int pageSize = request.Size;
        int startIndex = (currentPage - 1) * pageSize;
        int endIndex = totalCount == 0 ? 0 : Math.Min(startIndex + pageSize - 1, totalCount - 1);

        return new VehiclesResponse(vehicles, new Pagination(totalCount, pageSize, currentPage, totalPages, startIndex, endIndex));
    }
}
