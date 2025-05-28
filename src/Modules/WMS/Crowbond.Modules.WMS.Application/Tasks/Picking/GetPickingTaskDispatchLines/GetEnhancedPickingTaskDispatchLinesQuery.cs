using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Tasks.Picking.GetPickingTaskDispatchLines;

public sealed record GetEnhancedPickingTaskDispatchLinesQuery(Guid TaskHeaderId) : IQuery<IReadOnlyCollection<EnhancedTaskDispatchLineResponse>>;