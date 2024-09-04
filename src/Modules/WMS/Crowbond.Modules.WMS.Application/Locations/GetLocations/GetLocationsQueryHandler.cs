using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Dapper;

namespace Crowbond.Modules.WMS.Application.Locations.GetLocations;

internal sealed class GetLocationsQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetLocationsQuery, IReadOnlyCollection<LocationResponse>>
{
    public async Task<Result<IReadOnlyCollection<LocationResponse>>> Handle(GetLocationsQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 id AS {nameof(LocationResponse.Id)},
                 name AS {nameof(LocationResponse.Name)}
             FROM wms.locations
             WHERE
                location_layer = @LocationLayer
                AND location_Type = @LocationType
             """;

        List<LocationResponse> locations = (await connection.QueryAsync<LocationResponse>(sql, request)).AsList();

        return locations;
    }
}
