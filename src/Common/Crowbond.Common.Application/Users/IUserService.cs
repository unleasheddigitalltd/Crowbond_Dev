using Crowbond.Common.Domain;

namespace Crowbond.Common.Application.Users;

public interface IUserService
{
    Task<Result<UserResponse>> GetUserByIdentityIdAsync(string identityId);
}
