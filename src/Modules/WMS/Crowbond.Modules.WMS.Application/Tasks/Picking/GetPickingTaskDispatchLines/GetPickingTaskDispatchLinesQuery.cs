using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Tasks.Picking.GetPickingTaskDispatchLines;

public sealed record GetPickingTaskDispatchLinesQuery(Guid TaskHeaderId) : IQuery<IReadOnlyCollection<TaskDispatchLineResponse>>;
