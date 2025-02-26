using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Application.Users;
using Crowbond.Common.Domain;
using Crowbond.Modules.Users.Domain.Users;

namespace Crowbond.Modules.Users.Application.Users.GetUserByIdentityId;

internal sealed class GetUserByIdentityIdQueryHandler(IUserRepository userRepository)
    : IQueryHandler<GetUserByIdentityIdQuery, UserResponse>
{
    public async Task<Result<UserResponse>> Handle(
        GetUserByIdentityIdQuery request,
        CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdentityIdAsync(request.IdentityId, cancellationToken);
        
        if (user is null)
        {
            return Result.Failure<UserResponse>(Error.NotFound("User", request.IdentityId));
        }

        return Result.Success(new UserResponse(
            user.Id,
            user.Username,
            user.Email,
            user.FirstName,
            user.LastName,
            user.Mobile,
            user.IsActive,
            string.Join(",", user.Roles.Select(r => r.Name))));
    }
}