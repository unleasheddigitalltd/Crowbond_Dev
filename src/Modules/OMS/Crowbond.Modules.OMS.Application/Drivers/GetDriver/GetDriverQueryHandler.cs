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
                 u.id AS {nameof(DriverResponse.Id)},
                 u.first_name AS {nameof(DriverResponse.FirstName)},
                 u.last_name AS {nameof(DriverResponse.LastName)},
                 u.username AS {nameof(DriverResponse.Username)},
                 u.email AS {nameof(DriverResponse.Email)},
                 u.mobile AS {nameof(DriverResponse.Mobile)}
             FROM oms.drivers d
             INNER JOIN users u ON u.id = d.id
             WHERE d.id = @DriverId
             """;

        DriverResponse? driver = await connection.QuerySingleOrDefaultAsync<DriverResponse>(sql, request);

        if (driver is null)
        {
            return Result.Failure<DriverResponse>(DriverErrors.NotFound(request.DriverId));
        }

        return driver;
    }
}
