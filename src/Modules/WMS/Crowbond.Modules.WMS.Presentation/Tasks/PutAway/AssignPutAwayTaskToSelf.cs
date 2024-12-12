using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.WMS.Application.Abstractions.Authentication;
using Crowbond.Modules.WMS.Application.Tasks.PutAway.AssignPutAwayTask;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.WMS.Presentation.Tasks.PutAway;

internal sealed class AssignPutAwayTaskToSelf : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("tasks/put-away/assign-to-self", async (Request request, IWarehouseOperatorContext warehouseOperatorContext, ISender sender) =>
        {
            Result result = await sender.Send(new AssignPutAwayTaskCommand(
                request.TaskHeaderId,
                warehouseOperatorContext.WarehouseOperatorId));

            return result.Match(() => Results.Ok(), ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.ExecutePutAwayTasks)
            .WithTags(Tags.PutAway);
    }

    private sealed record Request(Guid TaskHeaderId);
}
