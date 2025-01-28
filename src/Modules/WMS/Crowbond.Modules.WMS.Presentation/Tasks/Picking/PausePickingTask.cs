using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.WMS.Application.Abstractions.Authentication;
using Crowbond.Modules.WMS.Application.Tasks.Picking.PausePickingTask;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.WMS.Presentation.Tasks.Picking;

internal sealed class PausePickingTask : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("tasks/picking/{id}/pause", async (IWarehouseOperatorContext operatorContext, Guid id, ISender sender) =>
        {
            Result result = await sender.Send(new PausePickingTaskCommand(operatorContext.WarehouseOperatorId, id));

            return result.Match(() => Results.Ok(), ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.ExecutePickingTasks)
            .WithTags(Tags.Picking);
    }
}
