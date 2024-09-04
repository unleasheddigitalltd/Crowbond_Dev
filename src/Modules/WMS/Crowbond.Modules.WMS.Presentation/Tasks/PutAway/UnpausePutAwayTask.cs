﻿using System.Security.Claims;
using Crowbond.Common.Domain;
using Crowbond.Common.Infrastructure.Authentication;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.WMS.Application.Tasks.PutAway.UnpausePutAwayTask;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.WMS.Presentation.Tasks.PutAway;

internal sealed class UnpausePutAwayTask : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("task/putaway/{id}/unpause", async (ClaimsPrincipal claims, Guid id, ISender sender) =>
        {
            Result result = await sender.Send(new UnpausePutAwayTaskCommand(claims.GetUserId(), id));

            return result.Match(Results.NoContent, ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.ExecutePutAwayTasks)
            .WithTags(Tags.PutAway);
    }
}
