using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Domain.Locations;
using Dapper;

namespace Crowbond.Modules.WMS.Application.Locations.GetLocationByScanCode;

internal sealed class GetLocationByScanCodeQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetLocationByScanCodeQuery, LocationResponse>
{
    public async Task<Result<LocationResponse>> Handle(GetLocationByScanCodeQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT 
                 id AS {nameof(LocationResponse.Id)},
                 parent_id AS {nameof(LocationResponse.ParentId)},
                 name AS {nameof(LocationResponse.Name)},
                 scan_code AS {nameof(LocationResponse.ScanCode)},
                 network_address AS {nameof(LocationResponse.NetworkAddress)},
                 printer_name AS {nameof(LocationResponse.PrinterName)},
                 location_type AS {nameof(LocationResponse.LocationType)},
                 location_layer AS {nameof(LocationResponse.LocationLayer)},
                 status AS {nameof(LocationResponse.Status)}
             FROM wms.locations          
             WHERE scan_code = @ScanCode
             """;

        LocationResponse? location = await connection.QuerySingleOrDefaultAsync<LocationResponse>(sql, request);
            
        if (location is null)
        {
            return Result.Failure<LocationResponse>(LocationErrors.SacnCodeNotFound(request.ScanCode));
        }        

        return location;
    }
}
