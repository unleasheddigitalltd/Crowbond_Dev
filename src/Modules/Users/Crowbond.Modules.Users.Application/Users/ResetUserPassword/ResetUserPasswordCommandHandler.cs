using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.Users.Application.Abstractions.Identity;
using Crowbond.Modules.Users.Domain.Users;

namespace Crowbond.Modules.Users.Application.Users.ResetUserPassword;

internal sealed class ResetUserPasswordCommandHandler(
    IIdentityProviderService identityProviderService,
    IUserRepository userRepository)
    : ICommandHandler<ResetUserPasswordCommand>
{
    public async Task<Result> Handle(ResetUserPasswordCommand request, CancellationToken cancellationToken)
    {
        User? user = await userRepository.GetByEmailAsync(request.Email, cancellationToken);

        if (user is null)
        {
            return Result.Failure(UserErrors.NotFound(request.Email));
        }

        Result result = await identityProviderService.ResetUserPasswordAsync(
            user.IdentityId,
            cancellationToken);

        if (result.IsFailure)
        {
            return Result.Failure(result.Error);
        }

        return Result.Success();
    }
}

