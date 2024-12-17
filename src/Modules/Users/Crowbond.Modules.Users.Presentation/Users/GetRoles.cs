using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.Users.Application.Users.GetRoles;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.Users.Presentation.Users;

internal sealed class GetRoles : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("users/roles", async (ISender sender) =>
        {
            Result<IReadOnlyCollection<RoleResponse>> result = await sender.Send(new GetRolesQuery());

            return result.Match(Results.Ok, ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.GetUser)
            .WithTags(Tags.Users);
    }
}
