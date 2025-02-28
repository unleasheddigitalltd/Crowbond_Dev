using Crowbond.Common.Application.Users;
using Crowbond.Common.Domain;
using Crowbond.Modules.Users.Application.Users.GetUserByIdentityId;
using MediatR;

namespace Crowbond.Modules.Users.Infrastructure.Users;

internal sealed class UserService(ISender sender) : IUserService
{
    public async Task<Result<UserResponse>> GetUserByIdentityIdAsync(string identityId)
    {
        return await sender.Send(new GetUserByIdentityIdQuery(identityId));
    }
}
