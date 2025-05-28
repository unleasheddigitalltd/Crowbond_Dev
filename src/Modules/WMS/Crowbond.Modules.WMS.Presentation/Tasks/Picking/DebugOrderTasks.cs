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

internal sealed class DebugOrderTasks : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("tasks/picking/order/{orderId}/debug", async (Guid orderId, IDbConnectionFactory dbConnectionFactory) =>
        {
            await using var connection = await dbConnectionFactory.OpenConnectionAsync();

            // Get all dispatch lines for this order
            var dispatchLines = await connection.QueryAsync<dynamic>(
                @"SELECT 
                    dl.id as dispatch_line_id,
                    dl.dispatch_header_id,
                    dl.order_id,
                    dl.order_no,
                    dl.customer_business_name,
                    dl.order_line_id,
                    dl.product_id,
                    dl.ordered_qty,
                    dl.picked_qty,
                    dl.is_bulk,
                    dl.is_picked,
                    dl.is_checked,
                    dh.dispatch_no,
                    dh.route_trip_id,
                    dh.route_name,
                    dh.status as dispatch_status
                  FROM wms.dispatch_lines dl
                  INNER JOIN wms.dispatch_headers dh ON dl.dispatch_header_id = dh.id
                  WHERE dl.order_id = @orderId
                  ORDER BY dh.dispatch_no, dl.order_line_id", new { orderId });

            // Get all tasks related to this order
            var tasks = await connection.QueryAsync<dynamic>(
                @"SELECT DISTINCT
                    th.id as task_id,
                    th.task_no,
                    th.dispatch_id,
                    th.task_type,
                    th.status as task_status,
                    dh.dispatch_no,
                    dh.route_name
                  FROM wms.task_headers th
                  INNER JOIN wms.dispatch_headers dh ON th.dispatch_id = dh.id
                  INNER JOIN wms.dispatch_lines dl ON dh.id = dl.dispatch_header_id
                  WHERE dl.order_id = @orderId
                  ORDER BY th.task_no", new { orderId });

            // Get all task lines and their mappings for this order
            var taskLinesWithMappings = await connection.QueryAsync<dynamic>(
                @"SELECT 
                    th.id as task_id,
                    tl.id as task_line_id,
                    tl.from_location_id,
                    fl.name as from_location_name,
                    tl.to_location_id,
                    tol.name as to_location_name,
                    tl.product_id,
                    p.name as product_name,
                    p.unit_of_measure_name,
                    tl.total_qty,
                    tl.completed_qty,
                    (tl.completed_qty >= tl.total_qty) as is_completed,
                    tldm.id as mapping_id,
                    tldm.dispatch_line_id,
                    tldm.allocated_qty,
                    tldm.completed_qty as mapping_completed_qty,
                    dl.order_line_id,
                    dl.ordered_qty,
                    dl.is_bulk
                  FROM wms.task_headers th
                  INNER JOIN wms.dispatch_headers dh ON th.dispatch_id = dh.id
                  INNER JOIN wms.dispatch_lines dl ON dh.id = dl.dispatch_header_id
                  INNER JOIN wms.task_lines tl ON th.id = tl.task_header_id
                  LEFT JOIN wms.task_line_dispatch_mappings tldm ON tl.id = tldm.task_line_id AND dl.id = tldm.dispatch_line_id
                  LEFT JOIN wms.locations fl ON tl.from_location_id = fl.id
                  LEFT JOIN wms.locations tol ON tl.to_location_id = tol.id
                  LEFT JOIN wms.products p ON tl.product_id = p.id
                  WHERE dl.order_id = @orderId
                  ORDER BY th.id, tl.id, tldm.id", new { orderId });

            // Get summary counts
            var summary = await connection.QuerySingleAsync<dynamic>(
                @"SELECT 
                    COUNT(DISTINCT dh.id) as dispatch_count,
                    COUNT(DISTINCT dl.id) as dispatch_line_count,
                    COUNT(DISTINCT th.id) as task_count,
                    COUNT(DISTINCT tl.id) as task_line_count,
                    COUNT(DISTINCT tldm.id) as mapping_count
                  FROM wms.dispatch_lines dl
                  INNER JOIN wms.dispatch_headers dh ON dl.dispatch_header_id = dh.id
                  LEFT JOIN wms.task_headers th ON dh.id = th.dispatch_id
                  LEFT JOIN wms.task_lines tl ON th.id = tl.task_header_id
                  LEFT JOIN wms.task_line_dispatch_mappings tldm ON tl.id = tldm.task_line_id
                  WHERE dl.order_id = @orderId", new { orderId });

            // Group task lines by task for better organization
            var taskLinesByTask = taskLinesWithMappings
                .GroupBy(tl => tl.task_id)
                .ToDictionary(
                    g => g.Key,
                    g => g.GroupBy(tl => tl.task_line_id)
                          .Select(tlg => new
                          {
                              TaskLineId = tlg.Key,
                              FromLocationId = tlg.First().from_location_id,
                              FromLocationName = tlg.First().from_location_name ?? "Unknown",
                              ToLocationId = tlg.First().to_location_id,
                              ToLocationName = tlg.First().to_location_name ?? "Unknown",
                              ProductId = tlg.First().product_id,
                              ProductName = tlg.First().product_name ?? "Unknown Product",
                              UnitOfMeasure = tlg.First().unit_of_measure_name ?? "Unknown",
                              TotalQty = tlg.First().total_qty,
                              CompletedQty = tlg.First().completed_qty,
                              IsCompleted = tlg.First().is_completed,
                              DispatchMappings = tlg.Where(m => m.mapping_id != null)
                                                   .Select(m => new
                                                   {
                                                       MappingId = m.mapping_id,
                                                       DispatchLineId = m.dispatch_line_id,
                                                       OrderLineId = m.order_line_id,
                                                       AllocatedQty = m.allocated_qty,
                                                       CompletedQty = m.mapping_completed_qty,
                                                       OrderedQty = m.ordered_qty,
                                                       IsBulk = m.is_bulk
                                                   }).ToList()
                          }).ToList()
                );

            return Results.Ok(new
            {
                OrderId = orderId,
                Summary = summary,
                DispatchLines = dispatchLines,
                Tasks = tasks,
                TaskLinesByTask = taskLinesByTask
            });
        })
            .RequireAuthorization(Permissions.ExecutePickingTasks)
            .WithTags(Tags.Picking);
    }
}