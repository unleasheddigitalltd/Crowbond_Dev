using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.WarehouseOperators.GetWarehouseOperatorActiveTaskAssignment;

public sealed record GetWarehouseOperatorActiveTaskAssignmentQuery(Guid WarehouseOperatorId) : IQuery<ActiveTaskAssignmentResponse>;
