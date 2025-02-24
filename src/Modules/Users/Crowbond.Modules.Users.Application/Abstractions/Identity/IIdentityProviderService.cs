using Crowbond.Common.Domain;
using Crowbond.Modules.Users.Application.Authentication;

namespace Crowbond.Modules.Users.Application.Abstractions.Identity;

public interface IIdentityProviderService
{
    Task<Result<string>> RegisterUserAsync(UserModel user, CancellationToken cancellationToken = default);

    Task<Result> UpdateUserAsync(string identityId, UserModel user, CancellationToken cancellationToken = default);

    Task<Result> ResetUserPasswordAsync(string identityId, CancellationToken cancellationToken = default);

    Task<Result> LogOutUserAsync(string identityId, CancellationToken cancellationToken = default);
    
    Task<Result> DeleteUser(string identityId, CancellationToken cancellationToken = default);

    Task<Result<AuthenticationResult>> AuthenticateAsync(string username, string password, CancellationToken cancellationToken = default);

    Task<Result<AuthenticationResult>> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default);
}
