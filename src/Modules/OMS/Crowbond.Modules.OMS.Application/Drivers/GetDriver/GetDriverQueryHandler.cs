using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Domain.Drivers;
using Dapper;

namespace Crowbond.Modules.OMS.Application.Drivers.GetDriver;

internal sealed class GetDriverQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetDriverQuery, DriverResponse>
{
    public async Task<Result<DriverResponse>> Handle(GetDriverQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 id AS {nameof(DriverResponse.Id)},
                 first_name AS {nameof(DriverResponse.FirstName)},
                 last_name AS {nameof(DriverResponse.LastName)},
                 username AS {nameof(DriverResponse.Username)},
                 email AS {nameof(DriverResponse.Email)},
                 mobile AS {nameof(DriverResponse.Mobile)},
                 vehicle_regn AS {nameof(DriverResponse.VehicleRegn)}
             FROM oms.drivers
             WHERE id = @DriverId
             """;

        DriverResponse? driver = await connection.QuerySingleOrDefaultAsync<DriverResponse>(sql, request);

        if (driver is null)
        {
            return Result.Failure<DriverResponse>(DriverErrors.NotFound(request.DriverId));
        }

        return driver;
    }
}
