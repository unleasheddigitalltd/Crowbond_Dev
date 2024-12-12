using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Dapper;

namespace Crowbond.Modules.WMS.Application.Tasks.PutAway.GetMyPutAwayTaskAssignments;

internal sealed class GetMyPutAwayTaskAssignmentsQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetMyPutAwayTaskAssignmentsQuery, IReadOnlyCollection<TaskAssignmentResponse>>
{
    public async Task<Result<IReadOnlyCollection<TaskAssignmentResponse>>> Handle(GetMyPutAwayTaskAssignmentsQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 t.id AS {nameof(TaskAssignmentResponse.Id)},
                 t.task_no AS {nameof(TaskAssignmentResponse.TaskNo)},
                 r.receipt_no AS {nameof(TaskAssignmentResponse.ReceiptNo)},
                 r.received_date AS {nameof(TaskAssignmentResponse.ReceivedDate)},
                 COUNT(rl.id) AS {nameof(TaskAssignmentResponse.TotalItems)},
                 COUNT(CASE WHEN rl.is_stored = true THEN 1 END) AS {nameof(TaskAssignmentResponse.RegisteredItems)},
                 ta.status AS {nameof(TaskAssignmentResponse.Status)}
             FROM wms.task_assignments ta
             INNER JOIN wms.task_headers t ON ta.task_header_id = t.id
             INNER JOIN wms.receipt_headers r ON r.id = t.receipt_id
             INNER JOIN wms.receipt_lines rl ON r.id = rl.receipt_header_id
             WHERE 
                t.task_type = 0 AND
                ta.assigned_operator_id = @WarehouseOperatorId AND
                ta.status IN (0, 1, 2)
             GROUP BY 
                ta.id, t.id, t.task_no, r.receipt_no, r.received_date, ta.status
             """;

        List<TaskAssignmentResponse> taskAssignments = (await connection.QueryAsync<TaskAssignmentResponse>(sql, request)).AsList();

        return taskAssignments;
    }
}
