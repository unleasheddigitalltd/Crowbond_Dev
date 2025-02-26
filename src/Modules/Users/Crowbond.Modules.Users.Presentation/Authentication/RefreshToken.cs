using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.Users.Application.Abstractions.Identity;
using Crowbond.Modules.Users.Application.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.Users.Presentation.Authentication;

internal sealed class RefreshToken : IEndpoint
{
    public sealed record RefreshTokenRequest(string RefreshToken, string Sub);

    public sealed record RefreshTokenResponse(
        string AccessToken,
        string IdToken,
        string RefreshToken,
        int ExpiresIn);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users/refresh-token", async (
            RefreshTokenRequest request,
            ClaimsPrincipal claimsPrincipal,
            IIdentityProviderService identityProviderService,
            CancellationToken cancellationToken) =>
        {
            Result<AuthenticationResult> result = await identityProviderService
                .RefreshTokenAsync(request.RefreshToken, request.Sub, cancellationToken);

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
        })
        .AllowAnonymous()
        .WithTags("Authentication");
    }
}

