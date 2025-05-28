using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Application.Abstractions.Data;
using Crowbond.Modules.WMS.Domain.Dispatches;
using Crowbond.Modules.WMS.Domain.Products;
using Crowbond.Modules.WMS.Domain.Tasks;

namespace Crowbond.Modules.WMS.Application.Tasks.Picking.Strategies;

/// <summary>
/// Strategy that consolidates dispatch lines by location and product for efficient picking
/// </summary>
public class ConsolidatedPickingStrategy : IPickingStrategy
{
    public async Task<Result> ProcessDispatch(
        DispatchHeader dispatch,
        IEnumerable<DispatchLine> dispatchLines,
        ConsolidatedPickingTaskService service,
        CancellationToken cancellationToken)
    {
        // Process item picking lines
        var lines = dispatchLines.ToList();
        var itemPickingResult = await service.ProcessDispatchLines(
            dispatch,
            lines.Where(l => !l.IsBulk),
            TaskType.PickingItem,
            cancellationToken);

        if (itemPickingResult.IsFailure)
        {
            return itemPickingResult;
        }

        // Process bulk picking lines
        var bulkPickingResult = await service.ProcessDispatchLines(
            dispatch,
            lines.Where(l => l.IsBulk),
            TaskType.PickingBulk,
            cancellationToken);

        return bulkPickingResult;
    }
}
