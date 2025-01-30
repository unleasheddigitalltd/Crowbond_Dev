using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.WMS.Application.Abstractions.Authentication;
using Crowbond.Modules.WMS.Application.Tasks.Checking.AssignCheckingTask;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.WMS.Presentation.Tasks.Checking;

internal sealed class AssignCheckingTaskToSelf : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("tasks/checking/assign-to-self", async (Request request, IWarehouseOperatorContext warehouseOperatorContext, ISender sender) =>
        {
            Result result = await sender.Send(new AssignCheckingTaskCommand(
                request.TaskHeaderId,
                warehouseOperatorContext.WarehouseOperatorId));

            return result.Match(() => Results.Ok(), ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.ExecuteCheckingTasks)
            .WithTags(Tags.Checking);
    }

    private sealed record Request(Guid TaskHeaderId);
}
