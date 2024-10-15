using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Application.Pagination;
using Crowbond.Common.Domain;
using Dapper;

namespace Crowbond.Modules.WMS.Application.Tasks.PutAway.GetPutAwayTasks;

internal sealed class GetPutAwayTasksQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetPutAwayTasksQuery, PutAwayTasksResponse>
{
    public async Task<Result<PutAwayTasksResponse>> Handle(GetPutAwayTasksQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        string sortOrder = request.Order.Equals("ASC", StringComparison.OrdinalIgnoreCase) ? "ASC" : "DESC";
        string orderByClause = request.Sort switch
        {
            "TaskNo" => "t.task_no",
            "ReceiptNo" => "r.receipt_no",
            "ReceivedDate" => "r.received_date",
            "DeliveryNoteNumber" => "r.delivery_note_number",
            "Status" => "t.status",
            _ => "t.status" // Default sorting
        };

        string sql = $@"
            WITH FilteredPutAwayTasks AS (
                SELECT
                    t.id AS {nameof(PutAwayTask.Id)},
                    t.task_no AS {nameof(PutAwayTask.TaskNo)},
                    r.receipt_no AS {nameof(PutAwayTask.ReceiptNo)},
                    r.received_date AS {nameof(PutAwayTask.ReceivedDate)},
                    r.delivery_note_number AS {nameof(PutAwayTask.DeliveryNoteNumber)}, 
                    t.status AS {nameof(PutAwayTask.Status)},
                    ROW_NUMBER() OVER (ORDER BY {orderByClause} {sortOrder}) AS RowNum
                FROM wms.task_headers t
                INNER JOIN wms.receipt_headers r ON r.id = t.receipt_id
                WHERE
                    t.task_type = 0 AND
                    (t.task_no ILIKE '%' || @Search || '%'
                    OR r.receipt_no ILIKE '%' || @Search || '%'   
                    OR r.delivery_note_number ILIKE '%' || @Search || '%')

            )
            SELECT 
                t.{nameof(PutAwayTask.Id)},
                t.{nameof(PutAwayTask.TaskNo)},
                t.{nameof(PutAwayTask.ReceiptNo)},
                t.{nameof(PutAwayTask.ReceivedDate)},
                t.{nameof(PutAwayTask.DeliveryNoteNumber)},
                t.{nameof(PutAwayTask.Status)}
            FROM FilteredPutAwayTasks t
            WHERE t.RowNum BETWEEN ((@Page) * @Size) + 1 AND (@Page + 1) * @Size
            ORDER BY t.RowNum;

            SELECT Count(*) 
                FROM wms.task_headers t
                INNER JOIN wms.receipt_headers r ON r.id = t.receipt_id
                WHERE
                    t.task_type = 0 AND
                    (t.task_no ILIKE '%' || @Search || '%'
                    OR r.receipt_no ILIKE '%' || @Search || '%'  
                    OR r.delivery_note_number ILIKE '%' || @Search || '%')
                    
        ";

        SqlMapper.GridReader multi = await connection.QueryMultipleAsync(sql, request);

        var putAwayTasks = (await multi.ReadAsync<PutAwayTask>()).ToList();
        int totalCount = await multi.ReadSingleAsync<int>();

        int totalPages = (int)Math.Ceiling(totalCount / (double)request.Size);
        int currentPage = request.Page;
        int pageSize = request.Size;
        int startIndex = currentPage * pageSize;
        int endIndex = Math.Min(startIndex + pageSize - 1, totalCount - 1);

        return new PutAwayTasksResponse(putAwayTasks, new Pagination(totalCount, pageSize, currentPage, totalPages, startIndex, endIndex));
    }
}
