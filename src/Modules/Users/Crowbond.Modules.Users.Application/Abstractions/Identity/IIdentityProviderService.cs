using Crowbond.Common.Domain;

namespace Crowbond.Modules.Users.Application.Abstractions.Identity;

public interface IIdentityProviderService
{
    Task<Result<string>> RegisterUserAsync(UserModel user, CancellationToken cancellationToken = default);

    Task<Result> UpdateUserAsync(string identityId, UserModel user, CancellationToken cancellationToken = default);

    Task<Result> ResetUserPasswordAsync(string identityId, CancellationToken cancellationToken = default);

    Task<Result> LogOutUserAsync(string identityId, CancellationToken cancellationToken = default);
    
    Task<Result> DeleteUser(string identityId, CancellationToken cancellationToken = default);
}
