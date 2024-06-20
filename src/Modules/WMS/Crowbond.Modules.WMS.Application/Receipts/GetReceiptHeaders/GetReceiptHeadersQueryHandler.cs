using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Application.Receipts.GetReceipts;
using Crowbond.Modules.WMS.Application.Receipts.GetReceipts.Dtos;
using ReceiptStatus = Crowbond.Modules.WMS.Domain.Receipts.ReceiptStatus;
using Dapper;
using System.Text;

namespace Crowbond.Modules.WMS.Application.Receipts.GetReceiptHeaders;

internal sealed class GetReceiptHeadersQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetReceiptHeadersQuery, ReceiptHeadersResponse>
{
    public async Task<Result<ReceiptHeadersResponse>> Handle(GetReceiptHeadersQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        string sortOrder = request.Order.Equals("ASC", StringComparison.OrdinalIgnoreCase) ? "ASC" : "DESC";
        string orderByClause = request.Sort switch
        {
            "receivedDate" => "received_date",
            "purchaseOrderNumber" => "received_date",
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
                    received_date AS {nameof(ReceiptHeader.ReceivedDate)},
                    delivery_note_number AS {nameof(ReceiptHeader.DeliveryNoteNumber)},
                    CASE status {string.Join(" ", caseClauses)} ELSE 'Unknown' END AS {nameof(ReceiptHeader.Status)},
                    ROW_NUMBER() OVER (ORDER BY {orderByClause} {sortOrder}) AS RowNum
                FROM wms.receipt_headers
                WHERE
                    delivery_note_number ILIKE '%' || @Search || '%'
            )
            SELECT 
                r.{nameof(ReceiptHeader.Id)},
                r.{nameof(ReceiptHeader.ReceivedDate)},
                r.{nameof(ReceiptHeader.DeliveryNoteNumber)},
                r.{nameof(ReceiptHeader.Status)}
            FROM FilteredReceiptHeaders r
            WHERE r.RowNum BETWEEN ((@Page) * @Size) + 1 AND (@Page + 1) * @Size
            ORDER BY r.RowNum;

            SELECT COUNT(*)
                FROM wms.receipt_headers
                WHERE
                    delivery_note_number ILIKE '%' || @Search || '%'
        ";

        SqlMapper.GridReader multi = await connection.QueryMultipleAsync(sql, request);

        var receipts = (await multi.ReadAsync<ReceiptHeader>()).ToList();
        int totalCount = await multi.ReadSingleAsync<int>();

        int totalPages = (int)Math.Ceiling(totalCount / (double)request.Size);
        int currentPage = request.Page;
        int pageSize = request.Size;
        int startIndex = (currentPage - 1) * pageSize;
        int endIndex = Math.Min(startIndex + pageSize - 1, totalCount - 1);

        return new ReceiptHeadersResponse(receipts, new Pagination(totalCount, pageSize, currentPage, totalPages, startIndex, endIndex));
    }
}
