using System.Security.Claims;
using Crowbond.Common.Domain;
using Crowbond.Common.Infrastructure.Authentication;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.WMS.Application.Tasks.PutAway.LocateProductForPutAway;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.WMS.Presentation.Tasks.PutAway;

internal sealed class LocateProductForPutAway : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("task/putaway/{taskId}/locate", async (ClaimsPrincipal claims, Guid taskId, Request request, ISender sender) =>
        {
            Result<Guid> result = await sender.Send(new LocateProductForPutAwayCommand(
                claims.GetUserId(),
                taskId,
                request.ProductId,
                request.LocationId,
                request.Qty));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.ExecutePutAwayTasks)
            .WithTags(Tags.PutAway);
    }

    private sealed record Request(Guid ProductId, Guid LocationId, decimal Qty);
}
