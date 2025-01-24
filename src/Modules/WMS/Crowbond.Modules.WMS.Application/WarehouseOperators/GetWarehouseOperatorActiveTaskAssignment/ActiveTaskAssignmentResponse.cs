namespace Crowbond.Modules.WMS.Application.WarehouseOperators.GetWarehouseOperatorActiveTaskAssignment;

public sealed record ActiveTaskAssignmentResponse(Guid WarehouseOperatorId, Guid? TaskId, string? TaskNo);
