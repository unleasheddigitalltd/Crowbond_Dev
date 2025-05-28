using Crowbond.Common.Application.Data;
using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.WMS.Application.Tasks;
using Crowbond.Modules.WMS.Domain.Dispatches;
using Dapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.WMS.Presentation.Tasks.Picking;

internal sealed class TriggerConsolidatedPicking : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("tasks/picking/{id}/trigger-consolidation", async (
            Guid id, 
            IDbConnectionFactory dbConnectionFactory,
            IDispatchRepository dispatchRepository,
            ConsolidatedPickingTaskService consolidatedPickingService) =>
        {
            await using var connection = await dbConnectionFactory.OpenConnectionAsync();

            // Get the dispatch ID for this task
            var dispatchId = await connection.QuerySingleOrDefaultAsync<Guid?>(
                "SELECT dispatch_id FROM wms.task_headers WHERE id = @id", new { id });

            if (!dispatchId.HasValue)
            {
                return Results.BadRequest("Task not found or no associated dispatch");
            }

            // Get the dispatch header
            var dispatch = await dispatchRepository.GetAsync(dispatchId.Value);
            if (dispatch == null)
            {
                return Results.BadRequest("Dispatch not found");
            }

            try
            {
                // Trigger the consolidated picking service
                var result = await consolidatedPickingService.AddDispatchToPickingTasks(dispatch);
                
                if (result.IsFailure)
                {
                    return Results.BadRequest($"Consolidation failed: {result.Error}");
                }

                // Check what was created
                var taskLineCount = await connection.QuerySingleOrDefaultAsync<int>(
                    "SELECT COUNT(*) FROM wms.task_lines tl INNER JOIN wms.task_headers th ON tl.task_header_id = th.id WHERE th.id = @id", 
                    new { id });

                var mappingCount = await connection.QuerySingleOrDefaultAsync<int>(
                    @"SELECT COUNT(*) 
                      FROM wms.task_line_dispatch_mappings tldm
                      INNER JOIN wms.task_lines tl ON tldm.task_line_id = tl.id
                      INNER JOIN wms.task_headers th ON tl.task_header_id = th.id
                      WHERE th.id = @id", 
                    new { id });

                return Results.Ok(new
                {
                    Message = "Consolidation triggered successfully",
                    TaskId = id,
                    DispatchId = dispatchId,
                    TaskLinesCreated = taskLineCount,
                    MappingsCreated = mappingCount
                });
            }
            catch (Exception ex)
            {
                return Results.BadRequest($"Error during consolidation: {ex.Message}");
            }
        })
            .RequireAuthorization(Permissions.ExecutePickingTasks)
            .WithTags(Tags.Picking);
    }
}