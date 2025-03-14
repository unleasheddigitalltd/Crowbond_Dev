using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Domain.Tasks;
using Dapper;

namespace Crowbond.Modules.WMS.Application.Tasks.Picking.GetMyPickingTaskAssignments;

internal sealed class GetMyPickingTaskAssignmentsQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetMyPickingTaskAssignmentsQuery, IReadOnlyCollection<TaskAssignmentResponse>>
{
    public async Task<Result<IReadOnlyCollection<TaskAssignmentResponse>>> Handle(GetMyPickingTaskAssignmentsQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        string[] caseClauses = Enum.GetValues<TaskType>()
                          //   .Cast<TaskType>()
                          .Select(status => $"WHEN {(int)status} THEN '{status}'")
                          .ToArray();

        string sql =
            $"""
             SELECT
                 t.id AS {nameof(TaskAssignmentResponse.TaskId)},
                 t.task_no AS {nameof(TaskAssignmentResponse.TaskNo)},
                 d.dispatch_no AS {nameof(TaskAssignmentResponse.DispatchNo)},
                 d.route_trip_id AS {nameof(TaskAssignmentResponse.RouteTripId)},
                 d.route_name AS {nameof(TaskAssignmentResponse.RouteName)},
                 d.route_trip_date AS {nameof(TaskAssignmentResponse.RouteTripDate)},
                 ta.status AS {nameof(TaskAssignmentResponse.Status)},
                 CASE t.task_type {string.Join(" ", caseClauses)} ELSE 'Unknown' END AS {nameof(TaskAssignmentResponse.TaskType)},
                 COUNT(dl.id) AS {nameof(TaskAssignmentResponse.TotalLines)}, 
                 SUM(CASE WHEN dl.is_picked = true THEN 1 ELSE 0 END) AS {nameof(TaskAssignmentResponse.FinishedLines)}
             FROM wms.task_headers t
             INNER JOIN wms.dispatch_headers d ON t.dispatch_id = d.id
             INNER JOIN wms.dispatch_lines dl ON d.id = dl.dispatch_header_id
             INNER JOIN wms.task_assignments ta ON ta.task_header_id = t.id
             WHERE 
                 t.task_type IN (1, 2)
                 AND ta.assigned_operator_id = @WarehouseOperatorId
                 AND ta.status IN (0, 1, 2)
             GROUP BY 
                 t.id, t.task_no, d.dispatch_no, d.route_trip_id, d.route_name, 
                 d.route_trip_date, ta.status, t.task_type
             ORDER BY t.task_no;
             """;

        List<TaskAssignmentResponse> taskAssignments = (await connection.QueryAsync<TaskAssignmentResponse>(sql, request)).AsList();

        return taskAssignments;
    }
}
