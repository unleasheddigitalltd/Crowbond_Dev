using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Domain.Receipts;
using Dapper;

namespace Crowbond.Modules.WMS.Application.Receipts.GetReceiptHeader;

internal sealed class GetReceiptHeaderQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetReceiptHeaderQuery, ReceiptResponse>
{
    public async Task<Result<ReceiptResponse>> Handle(GetReceiptHeaderQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        string[] caseClauses = Enum.GetValues(typeof(ReceiptStatus))
                          .Cast<ReceiptStatus>()
                          .Select(status => $"WHEN {(int)status} THEN '{status}'")
                          .ToArray();

        string sql =
            $"""
             SELECT 
                id AS {nameof(ReceiptResponse.Id)},
                receipt_no AS {nameof(ReceiptResponse.ReceiptNo)},
                received_date AS {nameof(ReceiptResponse.ReceivedDate)},
                purchase_order_no AS {nameof(ReceiptResponse.PurchaseOrderNo)},
                delivery_note_number AS {nameof(ReceiptResponse.DeliveryNoteNumber)},
                CASE status {string.Join(" ", caseClauses)} ELSE 'Unknown' END AS {nameof(ReceiptResponse.Status)}
             FROM wms.receipt_headers
             WHERE id = @ReceiptHeaderId
             """;

        ReceiptResponse? receipt = await connection.QuerySingleOrDefaultAsync<ReceiptResponse>(sql, request);

        if (receipt is null)
        {
            return Result.Failure<ReceiptResponse>(ReceiptErrors.NotFound(request.ReceiptHeaderId));
        }

        return receipt;
    }
}
