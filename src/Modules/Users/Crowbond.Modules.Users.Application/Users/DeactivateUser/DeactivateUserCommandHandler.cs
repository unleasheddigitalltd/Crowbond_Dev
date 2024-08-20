using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.Users.Application.Abstractions.Data;
using Crowbond.Modules.Users.Application.Abstractions.Identity;
using Crowbond.Modules.Users.Domain.Users;

namespace Crowbond.Modules.Users.Application.Users.DeactivateUser;

internal sealed class DeactivateUserCommandHandler(
    IUserRepository userRepository,
    IIdentityProviderService identityProviderService,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeactivateUserCommand>
{
    public async Task<Result> Handle(DeactivateUserCommand request, CancellationToken cancellationToken)
    {
        User? user = await userRepository.GetAsync(request.UserId, cancellationToken);

        if (user == null)
        {
            return Result.Failure(UserErrors.NotFound(request.UserId));
        }
                
        Result result = await identityProviderService.DeleteUser(user.IdentityId, cancellationToken);

        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }

        user.Deactivate();
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
