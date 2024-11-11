using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.Users.Application.Users.LogOutUser;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.Users.Presentation.Users;
internal class LogOutUser : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users/logout", async (string username, ISender sender) =>
        {
            Result result = await sender.Send(new LogOutUserCommand(username));

            return result.Match(() => Results.Ok(), ApiResults.Problem);
        })
        .AllowAnonymous()
        .WithTags(Tags.Users);
    }
}
