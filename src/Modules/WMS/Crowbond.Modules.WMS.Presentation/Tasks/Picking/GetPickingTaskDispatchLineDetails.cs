using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.WMS.Application.Abstractions.Authentication;
using Crowbond.Modules.WMS.Application.Tasks.Picking.GetPickingTaskDispatchLineDetails;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.WMS.Presentation.Tasks.Picking;

internal sealed class GetPickingTaskDispatchLineDetails : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("tasks/picking/{id}/dispatch-lines/{dispatchLineId}", async (
            IWarehouseOperatorContext operatorContext,
            Guid id,
            Guid dispatchLineId,
            ISender sender) =>
        {
            Result<TaskDispatchLineResponse> result = await sender.Send(new GetPickingTaskDispatchLineDetailsQuery(operatorContext.WarehouseOperatorId, id, dispatchLineId));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.ExecutePickingTasks)
            .WithTags(Tags.Picking);
    }
}
