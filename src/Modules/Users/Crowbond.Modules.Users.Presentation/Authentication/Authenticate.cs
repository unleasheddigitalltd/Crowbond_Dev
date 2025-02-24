using Crowbond.Common.Domain;
using Crowbond.Modules.Users.Application.Abstractions.Identity;
using Crowbond.Modules.Users.Application.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Crowbond.Modules.Users.Presentation.Authentication;

public static class Authenticate
{
    public sealed record AuthenticateRequest(
        string Username,
        string Password);

    public sealed record AuthenticateResponse(
        string AccessToken,
        string IdToken,
        string RefreshToken,
        int ExpiresIn);

    [AllowAnonymous]
    [HttpPost("users/authenticate")]
    public static async Task<IResult> AuthenticateUser(
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
}
