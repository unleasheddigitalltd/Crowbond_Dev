using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Domain.Receipts;
using Dapper;

namespace Crowbond.Modules.WMS.Application.Receipts.GetReceiptHeaderDetails;

internal sealed class GetReceiptHeaderDetailsQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetReceiptHeaderDetailsQuery, ReceiptHeaderDetailsResponse>
{
    public async Task<Result<ReceiptHeaderDetailsResponse>> Handle(GetReceiptHeaderDetailsQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 id AS {nameof(ReceiptHeaderDetailsResponse.Id)},
                 receipt_no AS {nameof(ReceiptHeaderDetailsResponse.ReceiptNo)},
                 received_date AS {nameof(ReceiptHeaderDetailsResponse.ReceivedDate)},
                 purchase_order_id AS {nameof(ReceiptHeaderDetailsResponse.PurchaseOrderId)},
                 purchase_order_no AS {nameof(ReceiptHeaderDetailsResponse.PurchaseOrderNo)},
                 delivery_note_number AS {nameof(ReceiptHeaderDetailsResponse.DeliveryNoteNumber)},
                 location_id AS {nameof(ReceiptHeaderDetailsResponse.LocationId)},
                 status AS {nameof(ReceiptHeaderDetailsResponse.Status)}
             FROM wms.receipt_headers
             WHERE id = @ReceiptHeaderId
             """;

        ReceiptHeaderDetailsResponse? receiptHeader = await connection.QuerySingleOrDefaultAsync<ReceiptHeaderDetailsResponse>(sql, request);

        if (receiptHeader is null)
        {
            return Result.Failure<ReceiptHeaderDetailsResponse>(ReceiptErrors.NotFound(request.ReceiptHeaderId));
        }

        return receiptHeader;
    }
}
