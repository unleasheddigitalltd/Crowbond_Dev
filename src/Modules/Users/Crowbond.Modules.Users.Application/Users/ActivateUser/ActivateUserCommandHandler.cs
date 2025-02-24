using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.Users.Application.Abstractions.Data;
using Crowbond.Modules.Users.Application.Abstractions.Identity;
using Crowbond.Modules.Users.Domain.Users;

namespace Crowbond.Modules.Users.Application.Users.ActivateUser;

internal sealed class ActivateUserCommandHandler(
    IUserRepository userRepository,
    IIdentityProviderService identityProviderService,
    IUnitOfWork unitOfWork)
    : ICommandHandler<ActivateUserCommand>
{
    public async Task<Result> Handle(ActivateUserCommand request, CancellationToken cancellationToken)
    {
        User? user = await userRepository.GetAsync(request.UserId, cancellationToken);

        if (user == null)
        {
            return Result.Failure(UserErrors.NotFound(request.UserId));
        }

        var result = await identityProviderService.UpdateUserAsync(user.IdentityId,
            new UserModel(user.Username, user.Email, string.Empty, user.FirstName, user.LastName, user.Mobile, true),
            cancellationToken);

        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }

        user.Activate();

        await unitOfWork.SaveChangesAsync(cancellationToken);

        var resetPasswordResult = await identityProviderService.ResetUserPasswordAsync(
            user.IdentityId,
            cancellationToken);

        if (resetPasswordResult.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }

        return Result.Success();
    }
}
