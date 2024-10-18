using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Tasks.Picking.GetMyPickingTaskAssignmentLineDatails;

public sealed record GetMyPickingTaskAssignmentLineDatailsQuery(Guid WarehouseOperatorId, Guid TaskAssignmentLineId) : IQuery<TaskAssignmentLineDetailsResponse>;
