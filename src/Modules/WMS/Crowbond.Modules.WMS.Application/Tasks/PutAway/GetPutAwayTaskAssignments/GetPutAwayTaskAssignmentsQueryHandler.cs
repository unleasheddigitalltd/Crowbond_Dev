using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Dapper;

namespace Crowbond.Modules.WMS.Application.Tasks.PutAway.GetPutAwayTaskAssignments;

internal sealed class GetPutAwayTaskAssignmentsQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : ICommandHandler<GetPutAwayTaskAssignmentsQuery, IReadOnlyCollection<TaskAssignmentResponse>>
{
    public async Task<Result<IReadOnlyCollection<TaskAssignmentResponse>>> Handle(GetPutAwayTaskAssignmentsQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 t.id AS {nameof(TaskAssignmentResponse.TaskId)},
                 t.task_no AS {nameof(TaskAssignmentResponse.TaskNo)},
                 ta.status AS {nameof(TaskAssignmentResponse.Status)}
             FROM wms.task_assignments ta
             INNER JOIN wms.task_headers t ON ta.task_header_id = t.id
             WHERE 
                ta.assigned_operator_id = @WarehouseOperatorId AND
                ta.status IN (0, 1, 2)
             """;

        List<TaskAssignmentResponse> taskAssignments = (await connection.QueryAsync<TaskAssignmentResponse>(sql, request)).AsList();

        return taskAssignments;
    }
}
