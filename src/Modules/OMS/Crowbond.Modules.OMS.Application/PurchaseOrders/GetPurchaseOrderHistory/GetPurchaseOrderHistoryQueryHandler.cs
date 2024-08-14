using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Dapper;

namespace Crowbond.Modules.OMS.Application.PurchaseOrders.GetPurchaseOrderHistory;

internal sealed class GetPurchaseOrderHistoryQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetPurchaseOrderHistoryQuery, IReadOnlyCollection<PurchaseOrderHistoryResponse>>
{
    public async Task<Result<IReadOnlyCollection<PurchaseOrderHistoryResponse>>> Handle(GetPurchaseOrderHistoryQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                id AS {nameof(PurchaseOrderHistoryResponse.Id)},
                purchase_order_header_id AS {nameof(PurchaseOrderHistoryResponse.PurchaseOrderHeaderId)},
                status AS {nameof(PurchaseOrderHistoryResponse.Status)}, 
                changed_at AS {nameof(PurchaseOrderHistoryResponse.ChangedAt)},
                changed_by AS {nameof(PurchaseOrderHistoryResponse.ChangedBy)}
             FROM 
                oms.purchase_order_status_histories
             WHERE
                purchase_order_header_id = @PurchaseOrderHeaderId
             """;

        List<PurchaseOrderHistoryResponse> purchaseOrderHistories = (await connection.QueryAsync<PurchaseOrderHistoryResponse>(sql, request)).AsList();

        return purchaseOrderHistories;
    }
}
