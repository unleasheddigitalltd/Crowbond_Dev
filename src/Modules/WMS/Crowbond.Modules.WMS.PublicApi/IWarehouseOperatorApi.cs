namespace Crowbond.Modules.WMS.PublicApi;

public interface IWarehouseOperatorApi
{
    Task<ActiveTaskAssignmentResponse> GetWarehouseOperatorActiveTaskAssignmentAsync(Guid warehouseOperatorId, CancellationToken cancellationToken = default);
}

public sealed record ActiveTaskAssignmentResponse(Guid WarehouseOperatorId, Guid? TaskId, string? TaskNo);
