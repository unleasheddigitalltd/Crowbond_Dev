using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Tasks.PutAway.GetPutAwayTaskAssignments;

public sealed record GetPutAwayTaskAssignmentsQuery(Guid WarehouseOperatorId) : ICommand<IReadOnlyCollection<TaskAssignmentResponse>>;
