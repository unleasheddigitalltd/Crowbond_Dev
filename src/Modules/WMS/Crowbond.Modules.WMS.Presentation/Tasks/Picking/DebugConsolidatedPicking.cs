using Crowbond.Common.Application.Data;
using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.WMS.Application.Tasks;
using Crowbond.Modules.WMS.Application.Tasks.Picking.Strategies;
using Crowbond.Modules.WMS.Domain.Dispatches;
using Crowbond.Modules.WMS.Domain.Products;
using Crowbond.Modules.WMS.Domain.Stocks;
using Dapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.WMS.Presentation.Tasks.Picking;

internal sealed class DebugConsolidatedPicking : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("tasks/picking/{id}/debug-consolidation", async (
            Guid id, 
            IDbConnectionFactory dbConnectionFactory,
            IDispatchRepository dispatchRepository,
            IProductRepository productRepository,
            IStockRepository stockRepository,
            ConsolidatedPickingTaskService consolidatedPickingService,
            IProductPickingClassifier productPickingClassifier,
            CancellationToken cancellationToken) =>
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
            var dispatch = await dispatchRepository.GetAsync(dispatchId.Value, cancellationToken);
            if (dispatch == null)
            {
                return Results.BadRequest("Dispatch not found");
            }

            var dispatchLines = new List<object>();

            // Analyze each dispatch line
            foreach (var line in dispatch.Lines)
            {
                var product = await productRepository.GetAsync(line.ProductId, cancellationToken);
                var requiresIndividualPicking = product != null && 
                    productPickingClassifier.RequiresIndividualPicking(product);

                // Get location information using the same logic as the service
                var resolvedLocationId = await consolidatedPickingService.GetProductLocationId(line.ProductId, cancellationToken);
                
                // Get stock information
                var stocks = await stockRepository.GetByProductAsync(line.ProductId, cancellationToken);
                var stockLocations = stocks.Select(s => new 
                { 
                    LocationId = s.LocationId, 
                    CurrentQty = s.CurrentQty 
                }).ToList();

                // Get location name
                var locationName = "Unknown";
                var locationSource = "Fallback (Unknown)";
                
                if (resolvedLocationId.HasValue)
                {
                    var location = await connection.QuerySingleOrDefaultAsync<string>(
                        "SELECT name FROM wms.locations WHERE id = @locationId", 
                        new { locationId = resolvedLocationId.Value });
                    
                    if (location != null)
                    {
                        locationName = location;
                        
                        // Determine source of location
                        if (stockLocations.Any(s => s.LocationId == resolvedLocationId.Value))
                        {
                            locationSource = "Stock Location";
                        }
                        else if (product?.DefaultLocation == resolvedLocationId.Value)
                        {
                            locationSource = "Product Default Location";
                        }
                        else if (resolvedLocationId.Value.ToString() == "00000000-0000-0000-0000-000000000001")
                        {
                            locationSource = "Fallback (Unknown)";
                            locationName = "Unknown Location";
                        }
                        else
                        {
                            locationSource = "Other";
                        }
                    }
                }

                var lineInfo = new
                {
                    DispatchLineId = line.Id,
                    ProductId = line.ProductId,
                    ProductName = product?.Name ?? "Product not found",
                    ProductUnitOfMeasure = product?.UnitOfMeasureName ?? "Unknown",
                    OrderedQty = line.OrderedQty,
                    IsBulk = line.IsBulk,
                    RequiresIndividualPicking = requiresIndividualPicking,
                    ProductExists = product != null,
                    DefaultLocation = product?.DefaultLocation,
                    StockLocations = stockLocations,
                    ResolvedLocationId = resolvedLocationId,
                    ResolvedLocationName = locationName,
                    LocationSource = locationSource
                };

                dispatchLines.Add(lineInfo);
            }

            // Check if there are any products that would be processed
            var itemLines = dispatch.Lines.Where(l => !l.IsBulk).ToList();
            var bulkLines = dispatch.Lines.Where(l => l.IsBulk).ToList();

            var summary = new
            {
                TaskId = id,
                DispatchId = dispatchId.Value,
                DispatchStatus = dispatch.Status.ToString(),
                TotalDispatchLines = dispatch.Lines.Count,
                ItemLines = itemLines.Count,
                BulkLines = bulkLines.Count,
                DispatchLines = dispatchLines
            };

            return Results.Ok(summary);
        })
            .RequireAuthorization(Permissions.ExecutePickingTasks)
            .WithTags(Tags.Picking);
    }
}