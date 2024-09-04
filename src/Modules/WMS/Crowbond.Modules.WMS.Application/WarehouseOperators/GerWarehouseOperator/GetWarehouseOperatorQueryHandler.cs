using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Domain.WarehouseOperators;
using Dapper;

namespace Crowbond.Modules.WMS.Application.WarehouseOperators.GerWarehouseOperator;

internal sealed class GetWarehouseOperatorQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetWarehouseOperatorQuery, WarehouseOperatorResponse>
{
    public async Task<Result<WarehouseOperatorResponse>> Handle(GetWarehouseOperatorQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 id AS {nameof(WarehouseOperatorResponse.Id)},
                 first_name AS {nameof(WarehouseOperatorResponse.FirstName)},
                 last_name AS {nameof(WarehouseOperatorResponse.LastName)},
                 username AS {nameof(WarehouseOperatorResponse.Username)},
                 email AS {nameof(WarehouseOperatorResponse.Email)},
                 mobile AS {nameof(WarehouseOperatorResponse.Mobile)}
             FROM wms.warehouse_operators
             WHERE id = @OperatorId
             """;

        WarehouseOperatorResponse? driver = await connection.QuerySingleOrDefaultAsync<WarehouseOperatorResponse>(sql, request);

        if (driver is null)
        {
            return Result.Failure<WarehouseOperatorResponse>(WarehouseOperatorErrors.NotFound(request.OperatorId));
        }

        return driver;
    }
}
