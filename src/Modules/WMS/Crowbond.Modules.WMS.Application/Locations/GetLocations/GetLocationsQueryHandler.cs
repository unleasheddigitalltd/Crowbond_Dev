using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Application.Pagination;
using Crowbond.Common.Domain;
using Dapper;

namespace Crowbond.Modules.WMS.Application.Locations.GetLocations;

internal sealed class GetLocationsQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetLocationsQuery, LocationsResponse>
{
    public async Task<Result<LocationsResponse>> Handle(GetLocationsQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        string sortOrder = request.Order.Equals("ASC", StringComparison.OrdinalIgnoreCase) ? "ASC" : "DESC";
        string orderByClause = request.Sort switch
        {
            "name" => "name",
            "scanCode" => "scan_code",
            "networkAddress" => "network_address",
            "printerName" => "printer_name",
            "locationType" => "location_type",
            "locationLayer" => "location_layer",
            "status" => "status",
            _ => "name" // Default sorting
        };

        string sql = $@"WITH FilteredLocations AS (
                SELECT 
                    id AS {nameof(Location.Id)},
                    parent_id AS {nameof(Location.ParentId)},
                    name AS {nameof(Location.Name)},
                    scan_code AS {nameof(Location.ScanCode)},
network_address AS {nameof(Location.NetworkAddress)},
                    printer_name AS {nameof(Location.PrinterName)},
                    location_type AS {nameof(Location.LocationType)},
                    location_layer AS {nameof(Location.LocationLayer)},
                    status AS {nameof(Location.Status)},
                    ROW_NUMBER() OVER (ORDER BY {orderByClause} {sortOrder}) AS RowNum
                FROM wms.locations
                WHERE
                    name ILIKE '%' || @Search || '%'
                    OR scan_code ILIKE '%' || @Search || '%'
            )
            SELECT 
                l.{nameof(Location.Id)},
                l.{nameof(Location.ParentId)},
                l.{nameof(Location.Name)},
                l.{nameof(Location.ScanCode)},
                l.{nameof(Location.NetworkAddress)} AS {nameof(Location.NetworkAddress)},
                l.{nameof(Location.PrinterName)} AS {nameof(Location.PrinterName)},
                l.{nameof(Location.LocationType)},
                l.{nameof(Location.LocationLayer)},
                l.{nameof(Location.Status)}
            FROM FilteredLocations l
            WHERE l.RowNum BETWEEN ((@Page) * @Size) + 1 AND (@Page + 1) * @Size
            ORDER BY l.RowNum;

            SELECT COUNT(*)
                FROM wms.locations
                WHERE
                    name ILIKE '%' || @Search || '%'
                    OR scan_code ILIKE '%' || @Search || '%'
        ";

        SqlMapper.GridReader multi = await connection.QueryMultipleAsync(sql, request);

        var receipts = (await multi.ReadAsync<Location>()).ToList();
        int totalCount = await multi.ReadSingleAsync<int>();

        int totalPages = (int)Math.Ceiling(totalCount / (double)request.Size);
        int currentPage = request.Page;
        int pageSize = request.Size;
        int startIndex = (currentPage - 1) * pageSize;
        int endIndex = totalCount == 0 ? 0 : Math.Min(startIndex + pageSize - 1, totalCount - 1);

        return new LocationsResponse(receipts, new Pagination(totalCount, pageSize, currentPage, totalPages, startIndex, endIndex));
    }
}
