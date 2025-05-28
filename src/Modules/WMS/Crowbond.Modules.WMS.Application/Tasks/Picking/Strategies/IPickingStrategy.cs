using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Domain.Dispatches;
using Crowbond.Modules.WMS.Domain.Tasks;

namespace Crowbond.Modules.WMS.Application.Tasks.Picking.Strategies;

public interface IPickingStrategy
{
    /// <summary>
    /// Processes dispatch and creates appropriate picking tasks
    /// </summary>
    /// <param name="dispatch">The dispatch header containing the lines</param>
    /// <param name="dispatchLines">The dispatch lines to process</param>
    /// <param name="service">The consolidated picking service for shared functionality</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Result indicating success or failure</returns>
    Task<Result> ProcessDispatch(
        DispatchHeader dispatch,
        IEnumerable<DispatchLine> dispatchLines,
        ConsolidatedPickingTaskService service,
        CancellationToken cancellationToken);
}