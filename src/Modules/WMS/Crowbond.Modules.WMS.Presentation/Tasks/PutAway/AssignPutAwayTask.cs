using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.WMS.Application.Tasks.PutAway.AssignPutAwayTask;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.WMS.Presentation.Tasks.PutAway;

internal sealed class AssignPutAwayTask : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("tasks/putaway/assign", async (Request request, ISender sender) =>
        {
            Result result = await sender.Send(new AssignPutAwayTaskCommand(
                request.TaskHeaderId,
                request.WarehouseOperatorId));

            return result.Match(() => Results.Ok(), ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.ManagePutAwayTasks)
            .WithTags(Tags.PutAway);
    }

    private sealed record Request(Guid TaskHeaderId, Guid WarehouseOperatorId);
}
