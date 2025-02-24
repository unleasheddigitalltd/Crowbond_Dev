using System.Net;
using Crowbond.Common.Domain;
using Crowbond.Modules.Users.Application.Abstractions.Identity;
using Crowbond.Modules.Users.Application.Authentication;
using Microsoft.Extensions.Logging;

namespace Crowbond.Modules.Users.Infrastructure.Identity;

internal sealed class IdentityProviderService(KeyCloakClient keyCloakClient, ILogger<IdentityProviderService> logger)
    : IIdentityProviderService
{
    private const string PasswordCredentialType = "password";

    // POST /admin/realms/{realm}/users
    public async Task<Result<string>> RegisterUserAsync(UserModel user, CancellationToken cancellationToken = default)
    {
        var userRepresentation = new UserRepresentation(
            user.Username,
            user.Email,
            user.FirstName,
            user.LastName,
            new UserAttribute(user.Mobile),
            true,
            user.Enabled,
            [new CredentialRepresentation(PasswordCredentialType, user.Password, false)]);

        try
        {
            string identityId = await keyCloakClient.RegisterUserAsync(userRepresentation, cancellationToken);

            return identityId;
        }
        catch (HttpRequestException exception) when (exception.StatusCode == HttpStatusCode.Conflict)
        {
            logger.LogError(exception, "User registration failed");

            return Result.Failure<string>(IdentityProviderErrors.EmailIsNotUnique);
        }
    }

    // PUT /admin/realms/{realm}/users/{id}/reset-password-email
    public async Task<Result> ResetUserPasswordAsync(string identityId, CancellationToken cancellationToken = default)
    {
        await keyCloakClient.ResetUserPasswordAsync(identityId, cancellationToken);
        return Result.Success();
    }

    // POST /admin/realms/{realm}/users/{id}/logout
    public async Task<Result> LogOutUserAsync(string identityId, CancellationToken cancellationToken = default)
    {
        await keyCloakClient.LogOutUserAsync(identityId, cancellationToken);
        return Result.Success();
    }

    // DELETE /{realm}/users/{id}
    public async Task<Result> DeleteUser(string identityId, CancellationToken cancellationToken = default)
    {
        await keyCloakClient.DeleteUserAsync(identityId, cancellationToken);
        return Result.Success();
    }

    // PUT /admin/realms/{realm}/users/{user-id}
    public async Task<Result> UpdateUserAsync(string identityId, UserModel user, CancellationToken cancellationToken = default)
    {
        var userRepresentation = new UserRepresentation(
            user.Username,
            user.Email,
            user.FirstName,
            user.LastName,
            new UserAttribute(user.Mobile),
            true,
            user.Enabled,
            []);

        await keyCloakClient.UpdateUserAsync(identityId, userRepresentation, cancellationToken);
        return Result.Success();
    }

    public async Task<Result<AuthenticationResult>> AuthenticateAsync(
        string username,
        string password,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var tokenResponse = await keyCloakClient.GetTokenAsync(username, password, cancellationToken);
            return new AuthenticationResult(
                tokenResponse.AccessToken,
                tokenResponse.IdToken,
                tokenResponse.RefreshToken,
                tokenResponse.ExpiresIn);
        }
        catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.Unauthorized)
        {
            logger.LogWarning(ex, "Invalid credentials for user {Username}", username);
            return Result.Failure<AuthenticationResult>(Error.Failure("Auth.InvalidCredentials", "Invalid credentials"));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error authenticating user {Username}", username);
            return Result.Failure<AuthenticationResult>(Error.Problem("Auth.Error", "An error occurred during authentication"));
        }
    }

    public async Task<Result<AuthenticationResult>> RefreshTokenAsync(
        string refreshToken,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var tokenResponse = await keyCloakClient.RefreshTokenAsync(refreshToken, cancellationToken);
            return new AuthenticationResult(
                tokenResponse.AccessToken,
                tokenResponse.IdToken,
                tokenResponse.RefreshToken,
                tokenResponse.ExpiresIn);
        }
        catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.Unauthorized)
        {
            logger.LogWarning(ex, "Invalid refresh token");
            return Result.Failure<AuthenticationResult>(Error.Failure("Auth.InvalidToken", "Invalid refresh token"));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error refreshing token");
            return Result.Failure<AuthenticationResult>(Error.Problem("Auth.Error", "An error occurred while refreshing the token"));
        }
    }
}
