using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Dapper;

namespace Crowbond.Modules.WMS.Application.Tasks.Picking.GetMyPickingTaskAssignments;

internal sealed class GetMyPickingTaskAssignmentsQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetMyPickingTaskAssignmentsQuery, IReadOnlyCollection<TaskAssignmentResponse>>
{
    public async Task<Result<IReadOnlyCollection<TaskAssignmentResponse>>> Handle(GetMyPickingTaskAssignmentsQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 t.id AS {nameof(TaskAssignmentResponse.TaskId)},
                 t.task_no AS {nameof(TaskAssignmentResponse.TaskNo)},
                 ta.status AS {nameof(TaskAssignmentResponse.Status)},
                 COUNT(l.id) AS {nameof(TaskAssignmentResponse.TotalLines)},
                 COUNT(CASE WHEN l.status IN (2, 3) THEN l.id END) AS {nameof(TaskAssignmentResponse.FinishedLines)}
             FROM wms.task_headers t
             INNER JOIN wms.task_assignments ta ON ta.task_header_id = t.id
             INNER JOIN wms.task_assignment_lines l ON ta.id = l.task_assignment_id
             WHERE 
                 t.task_type = 1
                 AND ta.assigned_operator_id = @WarehouseOperatorId
                 AND ta.status IN (0, 1, 2)
             GROUP BY t.id, t.task_no, ta.status
             ORDER BY t.task_no;
             """;

        List<TaskAssignmentResponse> taskAssignments = (await connection.QueryAsync<TaskAssignmentResponse>(sql, request)).AsList();

        return taskAssignments;
    }
}
