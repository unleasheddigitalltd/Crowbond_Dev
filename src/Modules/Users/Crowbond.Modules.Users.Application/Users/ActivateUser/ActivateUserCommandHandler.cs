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

        string password = Guid.NewGuid().ToString("N");
        Result<string> result = await identityProviderService.RegisterUserAsync(
            new UserModel(user.Username, user.Email, password, user.FirstName, user.LastName),
            cancellationToken);

        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }

        user.Activate(result.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        Result resetPasswordResult = await identityProviderService.ResetUserPasswordAsync(
            user.IdentityId,
            cancellationToken);

        if (resetPasswordResult.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }

        return Result.Success();
    }
}
