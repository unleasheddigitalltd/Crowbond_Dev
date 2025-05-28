using Crowbond.Common.Application.Data;
using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Dapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.WMS.Presentation.Tasks.Picking;

internal sealed class DebugTaskLines : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("tasks/picking/{id}/debug", async (Guid id, IDbConnectionFactory dbConnectionFactory) =>
        {
            await using var connection = await dbConnectionFactory.OpenConnectionAsync();

            var taskHeaderExists = await connection.QuerySingleOrDefaultAsync<bool>(
                "SELECT COUNT(*) > 0 FROM wms.task_headers WHERE id = @id", new { id });

            var taskLineCount = await connection.QuerySingleOrDefaultAsync<int>(
                "SELECT COUNT(*) FROM wms.task_lines tl INNER JOIN wms.task_headers th ON tl.task_header_id = th.id WHERE th.id = @id", new { id });

            var dispatchLineCount = await connection.QuerySingleOrDefaultAsync<int>(
                @"SELECT COUNT(*) 
                  FROM wms.task_headers t
                  INNER JOIN wms.dispatch_headers d ON t.dispatch_id = d.id
                  INNER JOIN wms.dispatch_lines dl ON dl.dispatch_header_id = d.id
                  WHERE t.id = @id", new { id });

            var taskType = await connection.QuerySingleOrDefaultAsync<int?>(
                "SELECT task_type FROM wms.task_headers WHERE id = @id", new { id });

            var mappingCount = await connection.QuerySingleOrDefaultAsync<int>(
                @"SELECT COUNT(*) 
                  FROM wms.task_line_dispatch_mappings tldm
                  INNER JOIN wms.task_lines tl ON tldm.task_line_id = tl.id
                  INNER JOIN wms.task_headers th ON tl.task_header_id = th.id
                  WHERE th.id = @id", new { id });

            return Results.Ok(new
            {
                TaskId = id,
                TaskHeaderExists = taskHeaderExists,
                TaskType = taskType,
                TaskLineCount = taskLineCount,
                DispatchLineCount = dispatchLineCount,
                MappingCount = mappingCount
            });
        })
            .RequireAuthorization(Permissions.ExecutePickingTasks)
            .WithTags(Tags.Picking);
    }
}