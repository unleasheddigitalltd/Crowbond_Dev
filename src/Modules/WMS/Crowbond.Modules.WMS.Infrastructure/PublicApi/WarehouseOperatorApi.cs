using Crowbond.Common.Domain;
using ActiveTaskAssignmentResponse = Crowbond.Modules.WMS.PublicApi.ActiveTaskAssignmentResponse;
using MediatR;
using Crowbond.Modules.WMS.PublicApi;
using Crowbond.Modules.WMS.Application.WarehouseOperators.GetWarehouseOperatorActiveTaskAssignment;

namespace Crowbond.Modules.WMS.Infrastructure.PublicApi;

public class WarehouseOperatorApi(ISender sender) : IWarehouseOperatorApi
{
    public async Task<ActiveTaskAssignmentResponse> GetWarehouseOperatorActiveTaskAssignmentAsync(
        Guid warehouseOperatorId, CancellationToken cancellationToken = default)
    {
        Result<Application.WarehouseOperators.GetWarehouseOperatorActiveTaskAssignment.ActiveTaskAssignmentResponse> result = 
            await sender.Send(new GetWarehouseOperatorActiveTaskAssignmentQuery(warehouseOperatorId), cancellationToken);

        if (result.IsFailure)
        {
            throw new InvalidOperationException(result.Error.Description);
        }

        return new ActiveTaskAssignmentResponse(
            result.Value.WarehouseOperatorId,
            result.Value.TaskId,
            result.Value.TaskNo);
    }
}
