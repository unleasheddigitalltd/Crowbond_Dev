using System.Net;
using Crowbond.Common.Domain;
using Crowbond.Modules.Users.Application.Abstractions.Identity;
using Crowbond.Modules.Users.Infrastructure.Identity;
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
            true,
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
}
