using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Dapper;

namespace Crowbond.Modules.OMS.Application.Orders.GetOrderLineRejectReasons;

internal sealed class GetOrderLineRejectReasonsQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetOrderLineRejectReasonsQuery, IReadOnlyCollection<RejectReasonResponse>>
{
    public async Task<Result<IReadOnlyCollection<RejectReasonResponse>>> Handle(GetOrderLineRejectReasonsQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 id AS {nameof(RejectReasonResponse.Id)},
                 title AS {nameof(RejectReasonResponse.Title)},
                 responsibility AS {nameof(RejectReasonResponse.Responsibility)},
                 is_active AS {nameof(RejectReasonResponse.IsActive)}
             FROM oms.order_line_reject_reasons
             """;

        List<RejectReasonResponse> reasons = (await connection.QueryAsync<RejectReasonResponse>(sql, request)).AsList();

        return reasons;
    }
}
