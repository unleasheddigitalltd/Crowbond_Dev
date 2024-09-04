using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Application.Receipts.GetReceipts;
using ReceiptStatus = Crowbond.Modules.WMS.Domain.Receipts.ReceiptStatus;
using Dapper;
using System.Text;
using Crowbond.Common.Application.Pagination;

namespace Crowbond.Modules.WMS.Application.Receipts.GetReceiptHeaders;

internal sealed class GetReceiptHeadersQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetReceiptHeadersQuery, ReceiptResponse>
{
    public async Task<Result<ReceiptResponse>> Handle(GetReceiptHeadersQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        string sortOrder = request.Order.Equals("ASC", StringComparison.OrdinalIgnoreCase) ? "ASC" : "DESC";
        string orderByClause = request.Sort switch
        {
            "receiptNo" => "receipt_no",
            "receivedDate" => "received_date",
            "purchaseOrderNo" => "purchase_order_no",
            "deliveryNoteNumber" => "delivery_note_number",
            "status" => "status",            
            _ => "receive_date" // Default sorting
        };

        string[] caseClauses = Enum.GetValues(typeof(ReceiptStatus))
                          .Cast<ReceiptStatus>()
                          .Select(status => $"WHEN {(int)status} THEN '{status}'")
                          .ToArray();

        string sql = $@"WITH FilteredReceiptHeaders AS (
                SELECT 
                    id AS {nameof(ReceiptHeader.Id)},
                    receipt_no AS {nameof(ReceiptHeader.ReceiptNo)},
                    received_date AS {nameof(ReceiptHeader.ReceivedDate)},
                    delivery_note_number AS {nameof(ReceiptHeader.DeliveryNoteNumber)},
                    purchase_order_no AS {nameof(ReceiptHeader.PurchaseOrderNo)},
                    CASE status {string.Join(" ", caseClauses)} ELSE 'Unknown' END AS {nameof(ReceiptHeader.Status)},
                    ROW_NUMBER() OVER (ORDER BY {orderByClause} {sortOrder}) AS RowNum
                FROM wms.receipt_headers
                WHERE
                    delivery_note_number ILIKE '%' || @Search || '%'
                    OR purchase_order_no ILIKE '%' || @Search || '%'
                    OR receipt_no ILIKE '%' || @Search || '%'
            )
            SELECT 
                r.{nameof(ReceiptHeader.Id)},
                r.{nameof(ReceiptHeader.ReceiptNo)},
                r.{nameof(ReceiptHeader.ReceivedDate)},
                r.{nameof(ReceiptHeader.DeliveryNoteNumber)},
                r.{nameof(ReceiptHeader.PurchaseOrderNo)},
                r.{nameof(ReceiptHeader.Status)}
            FROM FilteredReceiptHeaders r
            WHERE r.RowNum BETWEEN ((@Page) * @Size) + 1 AND (@Page + 1) * @Size
            ORDER BY r.RowNum;

            SELECT COUNT(*)
                FROM wms.receipt_headers
                WHERE
                    delivery_note_number ILIKE '%' || @Search || '%'
                    OR purchase_order_no ILIKE '%' || @Search || '%'
                    OR receipt_no ILIKE '%' || @Search || '%'
        ";

        SqlMapper.GridReader multi = await connection.QueryMultipleAsync(sql, request);

        var receipts = (await multi.ReadAsync<ReceiptHeader>()).ToList();
        int totalCount = await multi.ReadSingleAsync<int>();

        int totalPages = (int)Math.Ceiling(totalCount / (double)request.Size);
        int currentPage = request.Page;
        int pageSize = request.Size;
        int startIndex = (currentPage - 1) * pageSize;
        int endIndex = Math.Min(startIndex + pageSize - 1, totalCount - 1);

        return new ReceiptResponse(receipts, new Pagination(totalCount, pageSize, currentPage, totalPages, startIndex, endIndex));
    }
}
