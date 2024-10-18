using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Tasks.Picking.GetMyPickingTaskAssignments;

public sealed record GetMyPickingTaskAssignmentsQuery(Guid WarehouseOperatorId) : IQuery<IReadOnlyCollection<TaskAssignmentResponse>>;
