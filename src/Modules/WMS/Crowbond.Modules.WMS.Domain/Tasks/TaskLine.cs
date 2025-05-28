using Crowbond.Common.Domain;

namespace Crowbond.Modules.WMS.Domain.Tasks;

public sealed class TaskLine : Entity
{
    private readonly List<TaskLineDispatchMapping> _dispatchMappings = new();

    private TaskLine()
    {
    }

    public Guid Id { get; private set; }

    public Guid TaskHeaderId { get; private set; }

    public Guid FromLocationId { get; private set; }

    public Guid ToLocationId { get; private set; }

    public Guid ProductId { get; private set; }

    public decimal TotalQty { get; private set; }

    public decimal CompletedQty { get; private set; }

    public bool IsCompleted => CompletedQty >= TotalQty;

    public IReadOnlyCollection<TaskLineDispatchMapping> DispatchMappings => _dispatchMappings;

    public TaskHeader TaskHeader { get; }

    public static TaskLine Create(
        Guid fromLocationId,
        Guid toLocationId,
        Guid productId,
        decimal totalQty)
    {
        var taskLine = new TaskLine
        {
            Id = Guid.NewGuid(),
            FromLocationId = fromLocationId,
            ToLocationId = toLocationId,
            ProductId = productId,
            TotalQty = totalQty,
            CompletedQty = 0
        };

        return taskLine;
    }

    public Result AddDispatchMapping(Guid dispatchLineId, decimal allocatedQty)
    {
        if (_dispatchMappings.Any(m => m.DispatchLineId == dispatchLineId))
        {
            return Result.Failure(TaskErrors.DispatchLineAlreadyMapped(dispatchLineId));
        }

        var mapping = TaskLineDispatchMapping.Create(Id, dispatchLineId, allocatedQty);
        _dispatchMappings.Add(mapping);

        return Result.Success();
    }

    public Result CompleteQuantity(decimal qty)
    {
        if (CompletedQty + qty > TotalQty)
        {
            return Result.Failure(TaskErrors.ProductCompleteQtyExceedsRequestQty(ProductId));
        }

        // Update the task line's completed quantity
        CompletedQty += qty;
        
        // If there are dispatch mappings, distribute the completed quantity proportionally
        if (_dispatchMappings.Any())
        {
            // Calculate the proportion of the total quantity that each mapping represents
            var totalAllocated = _dispatchMappings.Sum(m => m.AllocatedQty);
            
            // Keep track of how much we've allocated to handle rounding errors
            decimal allocatedSoFar = 0;
            
            // For all but the last mapping, allocate proportionally
            for (int i = 0; i < _dispatchMappings.Count - 1; i++)
            {
                var mapping = _dispatchMappings[i];
                var proportion = mapping.AllocatedQty / totalAllocated;
                var mappingQty = Math.Round(qty * proportion, 2);
                
                // Update the mapping's completed quantity
                var result = mapping.CompleteQuantity(mappingQty);
                if (result.IsFailure)
                {
                    return result; // Propagate the error
                }
                
                allocatedSoFar += mappingQty;
            }
            
            // For the last mapping, allocate the remainder to avoid rounding errors
            var lastMapping = _dispatchMappings.Last();
            var remainingQty = qty - allocatedSoFar;
            
            var lastResult = lastMapping.CompleteQuantity(remainingQty);
            if (lastResult.IsFailure)
            {
                return lastResult; // Propagate the error
            }
        }
        
        return Result.Success();
    }
}
