using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.WMS.Application.Abstractions.Authentication;
using Crowbond.Modules.WMS.Application.Tasks.Picking.AssignPickingTask;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.WMS.Presentation.Tasks.Picking;

internal sealed class AssignPickingTaskToSelf : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("tasks/picking/assign-to-self", async (Request request, IWarehouseOperatorContext warehouseOperatorContext, ISender sender) =>
        {
            Result result = await sender.Send(new AssignPickingTaskCommand(
                request.TaskHeaderId,
                warehouseOperatorContext.WarehouseOperatorId));

            return result.Match(() => Results.Ok(), ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.ExecutePickingTasks)
            .WithTags(Tags.Picking);
    }

    private sealed record Request(Guid TaskHeaderId);
}
