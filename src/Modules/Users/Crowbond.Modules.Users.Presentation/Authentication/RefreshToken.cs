using Crowbond.Common.Domain;
using Crowbond.Modules.Users.Application.Abstractions.Identity;
using Crowbond.Modules.Users.Application.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Crowbond.Modules.Users.Presentation.Authentication;

public static class RefreshToken
{
    public sealed record RefreshTokenRequest(string RefreshToken);

    public sealed record RefreshTokenResponse(
        string AccessToken,
        string IdToken,
        string RefreshToken,
        int ExpiresIn);

    [AllowAnonymous]
    [HttpPost("users/refresh-token")]
    public static async Task<IResult> RefreshUserToken(
        [FromBody] RefreshTokenRequest request,
        IIdentityProviderService identityProviderService,
        CancellationToken cancellationToken)
    {
        Result<AuthenticationResult> result = await identityProviderService
            .RefreshTokenAsync(request.RefreshToken, cancellationToken);

        if (result.IsFailure)
        {
            return Results.BadRequest(result.Error);
        }

        var response = new RefreshTokenResponse(
            result.Value.AccessToken,
            result.Value.IdToken,
            result.Value.RefreshToken,
            result.Value.ExpiresIn);

        return Results.Ok(response);
    }
}
