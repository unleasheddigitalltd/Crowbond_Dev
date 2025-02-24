using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Modules.Users.Application.Abstractions.Identity;
using Crowbond.Modules.Users.Application.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.Users.Presentation.Authentication;

public sealed class Authenticate : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users/authenticate", AuthenticateUser)
            .AllowAnonymous()
            .WithTags("Authentication");
    }

    private static async Task<IResult> AuthenticateUser(
        [FromBody] AuthenticateRequest request,
        IIdentityProviderService identityProviderService,
        CancellationToken cancellationToken)
    {
        Result<AuthenticationResult> result = await identityProviderService
            .AuthenticateAsync(request.Username, request.Password, cancellationToken);

        if (result.IsFailure)
        {
            return Results.BadRequest(result.Error);
        }

        var response = new AuthenticateResponse(
            result.Value.AccessToken,
            result.Value.IdToken,
            result.Value.RefreshToken,
            result.Value.ExpiresIn);

        return Results.Ok(response);
    }

    public sealed record AuthenticateRequest(
        string Username,
        string Password);

    public sealed record AuthenticateResponse(
        string AccessToken,
        string IdToken,
        string RefreshToken,
        int ExpiresIn);
}
