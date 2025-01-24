using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.Users.Application.Abstractions.Data;
using Crowbond.Modules.Users.Application.Abstractions.Identity;
using Crowbond.Modules.Users.Domain.Users;

namespace Crowbond.Modules.Users.Application.Users.UpdateUser;

internal sealed class UpdateUserCommandHandler(
    IIdentityProviderService identityProviderService, 
    IUserRepository userRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateUserCommand>
{
    public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        User? user = await userRepository.GetAsync(request.UserId, cancellationToken);

        if (user is null)
        {
            return Result.Failure(UserErrors.NotFound(request.UserId));
        }

        Result result = await identityProviderService.UpdateUserAsync(
            user.IdentityId.ToString(),
            new UserModel(request.Username, request.Email, string.Empty, request.FirstName, request.LastName, request.Mobile, user.IsActive),
            cancellationToken);

        if (result.IsFailure)
        {
            return result;
        }

        user.Update(request.Username, request.Email, request.FirstName, request.LastName, request.Mobile);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
