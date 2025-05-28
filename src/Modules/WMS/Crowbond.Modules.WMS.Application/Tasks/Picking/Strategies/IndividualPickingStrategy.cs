using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Application.Abstractions.Data;
using Crowbond.Modules.WMS.Domain.Dispatches;
using Crowbond.Modules.WMS.Domain.Products;
using Crowbond.Modules.WMS.Domain.Tasks;

namespace Crowbond.Modules.WMS.Application.Tasks.Picking.Strategies;

/// <summary>
/// Strategy that creates individual picking tasks per order (no consolidation).
/// Used for products that require individual handling like fruit/veg that need weighing.
/// </summary>
public class IndividualPickingStrategy : IPickingStrategy
{
    public async Task<Result> ProcessDispatch(
        DispatchHeader dispatch,
        IEnumerable<DispatchLine> dispatchLines,
        ConsolidatedPickingTaskService service,
        CancellationToken cancellationToken)
    {
        var lines = dispatchLines.ToList();
        if (!lines.Any())
        {
            return Result.Success();
        }

        // For individual picking, we create a separate dispatch header for each order
        // This ensures fruit/veg products are picked separately per order for weighing
        var linesByOrder = lines.GroupBy(l => l.OrderId);

        foreach (var orderGroup in linesByOrder)
        {
            // For individual picking, we process each order's lines separately
            // but we use the original dispatch header - the service logic will still
            // create consolidated tasks but they'll be smaller (per order)
            
            // Process item picking lines for this order
            var itemPickingResult = await service.ProcessDispatchLines(
                dispatch,
                orderGroup.Where(l => !l.IsBulk),
                TaskType.PickingItem,
                cancellationToken);

            if (itemPickingResult.IsFailure)
            {
                return itemPickingResult;
            }

            // Process bulk picking lines for this order
            var bulkPickingResult = await service.ProcessDispatchLines(
                dispatch,
                orderGroup.Where(l => l.IsBulk),
                TaskType.PickingBulk,
                cancellationToken);

            if (bulkPickingResult.IsFailure)
            {
                return bulkPickingResult;
            }
        }

        return Result.Success();
    }
}