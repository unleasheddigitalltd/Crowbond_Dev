using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Tasks.Picking.GetMyPickingTaskAssignmentLines;

public sealed record GetMyPickingTaskAssignmentLinesQuery(Guid WarehouseOperatorId, Guid TaskHeaderId) : IQuery<IReadOnlyCollection<TaskAssignmentLineResponse>>;
