using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Application.Stocks.GetTransactionReasons.Dtos;
using Dapper;

namespace Crowbond.Modules.WMS.Application.Stocks.GetTransactionReasons;

internal sealed class GetReasonsQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetReasonsQuery, IReadOnlyCollection<ReasonResponse>>
{
    public async Task<Result<IReadOnlyCollection<ReasonResponse>>> Handle(GetReasonsQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 id AS {nameof(ReasonResponse.Id)},
                 name AS {nameof(ReasonResponse.Name)}
             FROM wms.stock_transaction_reasons
             WHERE action_type_name = @ActionTypeName
             """;

        List<ReasonResponse> reasonResponses = (await connection.QueryAsync<ReasonResponse>(sql, request)).AsList();

        return reasonResponses;
    }
}
