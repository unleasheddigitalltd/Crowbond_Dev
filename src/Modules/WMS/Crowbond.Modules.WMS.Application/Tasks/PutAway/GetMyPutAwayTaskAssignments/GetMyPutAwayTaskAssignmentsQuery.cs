using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Tasks.PutAway.GetMyPutAwayTaskAssignments;

public sealed record GetMyPutAwayTaskAssignmentsQuery(Guid WarehouseOperatorId) : IQuery<IReadOnlyCollection<TaskAssignmentResponse>>;
