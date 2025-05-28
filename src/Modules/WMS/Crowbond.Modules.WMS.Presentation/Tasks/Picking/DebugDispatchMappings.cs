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

internal sealed class DebugDispatchMappings : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("tasks/picking/{id}/debug-mappings", async (Guid id, IDbConnectionFactory dbConnectionFactory) =>
        {
            await using var connection = await dbConnectionFactory.OpenConnectionAsync();

            // Get the dispatch ID for this task
            var dispatchId = await connection.QuerySingleOrDefaultAsync<Guid?>(
                "SELECT dispatch_id FROM wms.task_headers WHERE id = @id", new { id });

            if (!dispatchId.HasValue)
            {
                return Results.BadRequest("Task not found or no associated dispatch");
            }

            // Get all dispatch lines for this dispatch
            var dispatchLines = await connection.QueryAsync<dynamic>(
                @"SELECT id, product_id, ordered_qty, is_bulk 
                  FROM wms.dispatch_lines 
                  WHERE dispatch_header_id = @dispatchId", new { dispatchId });

            // Check for any existing mappings for these dispatch lines
            var existingMappings = await connection.QueryAsync<dynamic>(
                @"SELECT 
                    dl.id as dispatch_line_id,
                    tldm.id as mapping_id,
                    tldm.task_line_id,
                    th.id as task_header_id,
                    th.task_type
                  FROM wms.dispatch_lines dl
                  LEFT JOIN wms.task_line_dispatch_mappings tldm ON dl.id = tldm.dispatch_line_id
                  LEFT JOIN wms.task_lines tl ON tldm.task_line_id = tl.id
                  LEFT JOIN wms.task_headers th ON tl.task_header_id = th.id
                  WHERE dl.dispatch_header_id = @dispatchId", new { dispatchId });

            // Check for any TaskLines in this specific task
            var taskLines = await connection.QueryAsync<dynamic>(
                @"SELECT tl.id, tl.from_location_id, tl.to_location_id, tl.product_id, tl.total_qty, tl.completed_qty,
                         (tl.completed_qty >= tl.total_qty) as is_completed
                  FROM wms.task_lines tl
                  INNER JOIN wms.task_headers th ON tl.task_header_id = th.id
                  WHERE th.id = @id", new { id });

            return Results.Ok(new
            {
                TaskId = id,
                DispatchId = dispatchId.Value,
                DispatchLines = dispatchLines,
                ExistingMappings = existingMappings,
                TaskLines = taskLines
            });
        })
            .RequireAuthorization(Permissions.ExecutePickingTasks)
            .WithTags(Tags.Picking);
    }
}