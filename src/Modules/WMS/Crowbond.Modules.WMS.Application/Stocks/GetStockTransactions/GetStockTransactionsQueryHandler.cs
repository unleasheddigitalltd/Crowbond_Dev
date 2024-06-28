using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Dapper;

namespace Crowbond.Modules.WMS.Application.Stocks.GetStockTransactions;

internal sealed class GetStockTransactionsQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetStockTransactionsQuery, IReadOnlyCollection<TransactionResponse>>
{
    public async Task<Result<IReadOnlyCollection<TransactionResponse>>> Handle(GetStockTransactionsQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                id AS {nameof(TransactionResponse.Id)},
                transaction_date AS {nameof(TransactionResponse.TransactionDate)},
                action_type_name AS {nameof(TransactionResponse.ActionType)},
                pos_adjustment AS {nameof(TransactionResponse.PosAdjustment)},
                quantity AS {nameof(TransactionResponse.Quantity)},
                transaction_note AS {nameof(TransactionResponse.TransactionNote)}

             FROM wms.stock_transactions
             WHERE stock_id = @StockId
             """;

        List<TransactionResponse> transactions = (await connection.QueryAsync<TransactionResponse>(sql, request)).AsList();

        return transactions;
    }
}
