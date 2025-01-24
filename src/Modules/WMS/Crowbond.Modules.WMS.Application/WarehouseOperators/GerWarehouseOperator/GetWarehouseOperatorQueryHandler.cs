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
                 w.id AS {nameof(WarehouseOperatorResponse.Id)},
                 u.first_name AS {nameof(WarehouseOperatorResponse.FirstName)},
                 u.last_name AS {nameof(WarehouseOperatorResponse.LastName)},
                 u.username AS {nameof(WarehouseOperatorResponse.Username)},
                 u.email AS {nameof(WarehouseOperatorResponse.Email)},
                 u.mobile AS {nameof(WarehouseOperatorResponse.Mobile)}
             FROM wms.warehouse_operators w
             INNER JOIN wms.users u ON u.id = w.id
             WHERE w.id = @OperatorId
             """;

        WarehouseOperatorResponse? driver = await connection.QuerySingleOrDefaultAsync<WarehouseOperatorResponse>(sql, request);

        if (driver is null)
        {
            return Result.Failure<WarehouseOperatorResponse>(WarehouseOperatorErrors.NotFound(request.OperatorId));
        }

        return driver;
    }
}
