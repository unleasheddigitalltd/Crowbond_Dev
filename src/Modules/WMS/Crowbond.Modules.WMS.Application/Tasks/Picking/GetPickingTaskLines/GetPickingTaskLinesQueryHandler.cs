using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Application.Tasks.Picking.GetPickingTaskDispatchLines;
using Dapper;

namespace Crowbond.Modules.WMS.Application.Tasks.Picking.GetPickingTaskLines;

internal sealed class GetPickingTaskLinesQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetPickingTaskLinesQuery, IReadOnlyCollection<TaskLineResponse>>
{
    public async Task<Result<IReadOnlyCollection<TaskLineResponse>>> Handle(GetPickingTaskLinesQuery request, CancellationToken cancellationToken)
    {
        await using var connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             WITH task_line_mappings AS (
                 SELECT 
                     tldm.task_line_id,
                     tldm.dispatch_line_id,
                     tldm.allocated_qty,
                     tldm.completed_qty,
                     dl.order_no,
                     dl.customer_business_name
                 FROM wms.task_line_dispatch_mappings tldm
                 INNER JOIN wms.dispatch_lines dl ON tldm.dispatch_line_id = dl.id
             )
             SELECT
                tl.id AS {nameof(TaskLineResponse.TaskLineId)},
                tl.from_location_id AS {nameof(TaskLineResponse.FromLocationId)},
                from_loc.name AS {nameof(TaskLineResponse.FromLocationName)},
                tl.to_location_id AS {nameof(TaskLineResponse.ToLocationId)},
                to_loc.name AS {nameof(TaskLineResponse.ToLocationName)},
                tl.product_id AS {nameof(TaskLineResponse.ProductId)},
                p.name AS {nameof(TaskLineResponse.ProductName)},
                p.unit_of_measure_name AS {nameof(TaskLineResponse.UnitOfMeasure)},
                tl.total_qty AS {nameof(TaskLineResponse.TotalQty)},
                tl.completed_qty AS {nameof(TaskLineResponse.CompletedQty)},
                (tl.completed_qty >= tl.total_qty) AS {nameof(TaskLineResponse.IsCompleted)},
                -- For the mappings (nullable when no mappings exist)
                COALESCE(tlm.dispatch_line_id, '00000000-0000-0000-0000-000000000000'::uuid) AS {nameof(TaskLineDispatchMappingResponse.DispatchLineId)},
                COALESCE(tlm.allocated_qty, 0) AS {nameof(TaskLineDispatchMappingResponse.AllocatedQty)},
                COALESCE(tlm.completed_qty, 0) AS {nameof(TaskLineDispatchMappingResponse.CompletedQty)},
                COALESCE(tlm.order_no, '') AS {nameof(TaskLineDispatchMappingResponse.OrderNo)},
                COALESCE(tlm.customer_business_name, '') AS {nameof(TaskLineDispatchMappingResponse.CustomerBusinessName)}
             FROM wms.task_lines tl
             INNER JOIN wms.task_headers th ON tl.task_header_id = th.id
             INNER JOIN wms.products p ON tl.product_id = p.id
             LEFT JOIN wms.locations from_loc ON tl.from_location_id = from_loc.id
             LEFT JOIN wms.locations to_loc ON tl.to_location_id = to_loc.id
             LEFT JOIN task_line_mappings tlm ON tlm.task_line_id = tl.id
             WHERE th.id = @TaskHeaderId
             ORDER BY tl.id, tlm.dispatch_line_id
             """;

        var taskLineLookup = new Dictionary<Guid, TaskLineResponse>();
        var mappings = new Dictionary<Guid, List<TaskLineDispatchMappingResponse>>();
        
        await connection.QueryAsync<TaskLineResponse, TaskLineDispatchMappingResponse, TaskLineResponse>(
            sql,
            (taskLine, mapping) =>
            {
                if (!taskLineLookup.TryGetValue(taskLine.TaskLineId, out var existingTaskLine))
                {
                    taskLineLookup[taskLine.TaskLineId] = taskLine;
                    mappings[taskLine.TaskLineId] = new List<TaskLineDispatchMappingResponse>();
                }

                // Only add mapping if it has a valid dispatch line ID (not the empty GUID)
                if (mapping.DispatchLineId != Guid.Empty)
                {
                    mappings[taskLine.TaskLineId].Add(mapping);
                }

                return taskLine;
            },
            request,
            splitOn: "DispatchLineId");

        // Build the final result with populated DispatchMappings
        var results = taskLineLookup.Values
            .Select(tl => tl with { DispatchMappings = mappings[tl.TaskLineId] })
            .ToList();

        return results;
    }
}