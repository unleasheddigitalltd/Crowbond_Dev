using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.Users.Application.Users.AssignRole;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.Users.Presentation.Users;

internal sealed class AssignRole : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("users/{id}/roles/assign", async (Guid id, Request request, ISender sender) =>
        {
            Result result = await sender.Send(new AssignRoleCommand(id, request.RoleName));

            return result.Match(() => Results.Ok(), ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.ModifyUser)
            .WithTags(Tags.Users);
    }

    internal sealed record Request(string RoleName);
}
