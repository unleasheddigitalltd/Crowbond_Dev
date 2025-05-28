using Crowbond.Common.Application.Messaging;
using Crowbond.Modules.WMS.Application.Tasks.Picking.GetPickingTaskDispatchLines;

namespace Crowbond.Modules.WMS.Application.Tasks.Picking.GetPickingTaskLines;

public sealed record GetPickingTaskLinesQuery(Guid TaskHeaderId) : IQuery<IReadOnlyCollection<TaskLineResponse>>;