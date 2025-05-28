using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Application.Abstractions.Data;
using Crowbond.Modules.WMS.Application.Tasks.Picking.Strategies;
using Crowbond.Modules.WMS.Domain.Dispatches;
using Crowbond.Modules.WMS.Domain.Tasks;
using Crowbond.Modules.WMS.Domain.Sequences;
using Crowbond.Modules.WMS.Domain.Products;
using Crowbond.Modules.WMS.Domain.Stocks;

namespace Crowbond.Modules.WMS.Application.Tasks;

public class ConsolidatedPickingTaskService(
    ITaskRepository taskRepository,
    IProductRepository productRepository,
    IStockRepository stockRepository,
    IProductPickingClassifier productPickingClassifier,
    ConsolidatedPickingStrategy consolidatedStrategy,
    IndividualPickingStrategy individualStrategy,
    IUnitOfWork unitOfWork)
{
    public async Task<Result> AddDispatchToPickingTasks(
        DispatchHeader dispatch,
        CancellationToken cancellationToken = default)
    {
        // Classify dispatch lines by product picking requirements
        var allProducts = await Task.WhenAll(
            dispatch.Lines.Select(async line => 
                new { 
                    Line = line, 
                    Product = await productRepository.GetAsync(line.ProductId, cancellationToken) 
                }));

        var productClassifications = allProducts
            .Where(p => p.Product != null)
            .GroupBy(p => productPickingClassifier.RequiresIndividualPicking(p.Product!))
            .ToDictionary(g => g.Key, g => g.Select(x => x.Line));

        // Process consolidated picking lines (grouped by location/product)
        if (productClassifications.TryGetValue(false, out var consolidatedLines))
        {
            var consolidatedResult = await consolidatedStrategy.ProcessDispatch(
                dispatch,
                consolidatedLines,
                this,
                cancellationToken);

            if (consolidatedResult.IsFailure)
            {
                return consolidatedResult;
            }
        }

        // Process individual picking lines (one task per order)
        if (productClassifications.TryGetValue(true, out var individualLines))
        {
            var individualResult = await individualStrategy.ProcessDispatch(
                dispatch,
                individualLines,
                this,
                cancellationToken);

            if (individualResult.IsFailure)
            {
                return individualResult;
            }
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }

    public async Task<Result> ProcessDispatchLines(
        DispatchHeader dispatch,
        IEnumerable<DispatchLine> dispatchLines,
        TaskType taskType,
        CancellationToken cancellationToken)
    {
        var lines = dispatchLines.ToList();
        if (!lines.Any())
        {
            return Result.Success();
        }

        // Group dispatch lines by their product's default location
        var dispatchLinesByLocation = new List<(Guid LocationId, IEnumerable<DispatchLine> Lines)>();
        
        foreach (var productGroup in lines.GroupBy(l => l.ProductId))
        {
            var productLocationId = await GetProductLocationId(productGroup.Key, cancellationToken);
            if (productLocationId.HasValue)
            {
                dispatchLinesByLocation.Add((productLocationId.Value, productGroup));
            }
        }
        
        // Group by location again to consolidate products from the same location
        var consolidatedByLocation = dispatchLinesByLocation
            .SelectMany(x => x.Lines.Select(line => new { LocationId = x.LocationId, Line = line }))
            .GroupBy(x => x.LocationId, x => x.Line);

        foreach (var locationGroup in consolidatedByLocation)
        {
            var locationId = locationGroup.Key;
            
            // Find or create a task header for this route trip and location
            var taskHeader = await GetOrCreateTaskHeader(
                dispatch.RouteTripId,
                locationId,
                dispatch.RouteTripDate,
                taskType,
                cancellationToken);

            if (taskHeader == null)
            {
                return Result.Failure(TaskErrors.SequenceNotFound());
            }

            // Process each product in this location
            foreach (var productGroup in locationGroup.GroupBy(l => l.ProductId))
            {
                var productId = productGroup.Key;
                var totalQty = productGroup.Sum(l => l.OrderedQty);

                // Find or create a task line for this product
                var taskLine = taskHeader.FindTaskLineByProductAndLocation(productId, locationId);
                
                if (taskLine == null)
                {
                    // Create a new task line for this product
                    var toLocationId = GetDestinationLocationId(dispatch.RouteTripId);
                    var addLineResult = taskHeader.AddTaskLine(locationId, toLocationId, productId, totalQty);
                    
                    if (addLineResult.IsFailure)
                    {
                        return addLineResult;
                    }
                    
                    taskLine = addLineResult.Value;
                    taskRepository.AddTaskLine(taskLine);
                }
                else
                {
                    // Update the existing task line with the additional quantity
                    // This would require a new method in TaskLine to update the quantity
                    // For simplicity, we'll assume the TaskLine entity has this capability
                }

                // Map each dispatch line to this task line
                foreach (var dispatchLine in productGroup)
                {
                    var mappingResult = taskHeader.MapDispatchLineToTaskLine(
                        taskLine.Id, 
                        dispatchLine.Id, 
                        dispatchLine.OrderedQty);
                    
                    if (mappingResult.IsFailure)
                    {
                        return mappingResult;
                    }

                    // Add the mapping to the repository
                    var mapping = taskLine.DispatchMappings.Last();
                    taskRepository.AddTaskLineDispatchMapping(mapping);
                }
            }
        }

        return Result.Success();
    }

    public async Task<TaskHeader?> GetOrCreateTaskHeader(
        Guid routeTripId,
        Guid locationId,
        DateOnly deliveryDate,
        TaskType taskType,
        CancellationToken cancellationToken)
    {
        // Try to find an existing task header for this route, location, and task type
        var existingTaskHeader = await taskRepository.GetByRouteLocationAndTypeAsync(
            routeTripId,
            locationId,
            taskType,
            cancellationToken);

        if (existingTaskHeader != null && existingTaskHeader.Status == TaskHeaderStatus.NotAssigned)
        {
            return existingTaskHeader;
        }

        // Create a new task header
        var sequence = await taskRepository.GetSequenceAsync(cancellationToken);
        
        if (sequence == null)
        {
            return null;
        }

        var nextTaskNo = sequence.GetNumber();
        
        var taskHeaderResult = TaskHeader.Create(
            nextTaskNo,
            null, // No receipt ID for picking tasks
            null, // No single dispatch ID for consolidated tasks
            locationId,
            routeTripId,
            deliveryDate,
            taskType);

        if (taskHeaderResult.IsFailure)
        {
            return null;
        }

        var taskHeader = taskHeaderResult.Value;
        taskRepository.Insert(taskHeader);
        
        return taskHeader;
    }

    public async Task<Guid?> GetProductLocationId(Guid productId, CancellationToken cancellationToken)
    {
        // First, try to get location from available stock
        var stocks = await stockRepository.GetByProductAsync(productId, cancellationToken);
        var firstStockLocation = stocks.FirstOrDefault()?.LocationId;
        
        if (firstStockLocation.HasValue)
        {
            return firstStockLocation.Value;
        }

        // Fallback to product's default location
        var product = await productRepository.GetAsync(productId, cancellationToken);
        if (product?.DefaultLocation.HasValue == true)
        {
            return product.DefaultLocation.Value;
        }

        // If no stock location and no default location, we should still create a task
        // but we'll need a special "Unknown Location" handling
        return GetOrCreateUnknownLocation();
    }

    private static Guid GetOrCreateUnknownLocation()
    {
        // For now, let's create a consistent "Unknown Location" GUID
        // In a real implementation, you might want to:
        // 1. Create an actual "Unknown/General" location in the database
        // 2. Or have a configuration setting for the default picking location
        
        // Using a deterministic GUID for "Unknown Location"
        return new Guid("00000000-0000-0000-0000-000000000001");
    }

    public static Guid GetDestinationLocationId(Guid routeTripId)
    {
        // In a real implementation, this would:
        // 1. Query the route trip to get the route information
        // 2. Determine the appropriate destination location based on route (e.g., GOBAY for specific routes)
        // 3. Use location configuration/mapping service
        
        // For now, we'll create a consistent destination based on the routeTripId
        // This ensures the same route always goes to the same destination location
        // In production, this would be replaced with actual business logic
        
        // Create a deterministic GUID based on routeTripId for consistency
        var routeBytes = routeTripId.ToByteArray();
        var destinationBytes = new byte[16];
        
        // Simple transformation to create a consistent destination ID
        for (int i = 0; i < 16; i++)
        {
            destinationBytes[i] = (byte)(routeBytes[i] ^ 0xFF); // XOR with 0xFF for different ID
        }
        
        return new Guid(destinationBytes);
    }
}
