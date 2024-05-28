using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.Users.Application.Abstractions.Identity;
using Crowbond.Modules.Users.Domain.Users;

namespace Crowbond.Modules.Users.Application.Users.LogOutUser;

internal sealed class LogOutUserCommandHandler(
    IIdentityProviderService identityProviderService,
    IUserRepository userRepository)
    : ICommandHandler<LogOutUserCommand>
{
    public async Task<Result> Handle(LogOutUserCommand request, CancellationToken cancellationToken)
    {
        User? user = await userRepository.GetByUsernameAsync(request.Username, cancellationToken);

        if (user is null)
        {
            return Result.Failure(UserErrors.NotFound(request.Username));
        }

        Result result = await identityProviderService.LogOutUserAsync(
            user.IdentityId,
            cancellationToken);

        if (result.IsFailure)
        {
            return Result.Failure(result.Error);
        }

        return Result.Success();
    }
}
