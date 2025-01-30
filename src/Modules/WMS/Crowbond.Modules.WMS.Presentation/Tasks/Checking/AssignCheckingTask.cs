using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.WMS.Application.Tasks.Checking.AssignCheckingTask;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.WMS.Presentation.Tasks.Checking;

internal sealed class AssignCheckingTask : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("tasks/checking/assign", async (Request request, ISender sender) =>
        {
            Result result = await sender.Send(new AssignCheckingTaskCommand(
                request.TaskHeaderId,
                request.WarehouseOperatorId));

            return result.Match(() => Results.Ok(), ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.ManageCheckingTasks)
            .WithTags(Tags.Checking);
    }

    private sealed record Request(Guid TaskHeaderId, Guid WarehouseOperatorId);
}
