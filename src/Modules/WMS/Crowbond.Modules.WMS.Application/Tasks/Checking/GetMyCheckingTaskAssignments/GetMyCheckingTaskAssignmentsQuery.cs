using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Tasks.Checking.GetMyCheckingTaskAssignments;

public sealed record GetMyCheckingTaskAssignmentsQuery(Guid WarehouseOperatorId) 
    : IQuery<IReadOnlyCollection<TaskAssignmentResponse>>;
