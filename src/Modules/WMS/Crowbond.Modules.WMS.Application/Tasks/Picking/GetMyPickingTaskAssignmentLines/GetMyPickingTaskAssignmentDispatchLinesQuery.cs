using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Tasks.Picking.GetMyPickingTaskAssignmentLines;

public sealed record GetMyPickingTaskAssignmentDispatchLinesQuery(Guid TaskHeaderId) : IQuery<IReadOnlyCollection<DispatchLineResponse>>;
