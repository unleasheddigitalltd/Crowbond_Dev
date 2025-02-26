using System.Security.Claims;
using Crowbond.Common.Domain;
using Crowbond.Common.Infrastructure.Authentication;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.Users.Application.Users.GetUser;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace Crowbond.Modules.Users.Presentation.Users;

internal sealed class GetUserProfile : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("users/profile", async (ClaimsPrincipal claims, ISender sender, ILogger<GetUserProfile> logger) =>
        {
            var userId = claims.GetUserId(); 
            logger.LogInformation("GetUserProfile endpoint called for user: {UserId}", userId);
            Result<UserResponse> result = await sender.Send(new GetUserQuery(userId));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .AllowAnonymous()
        .WithTags(Tags.Users);
    }
}
