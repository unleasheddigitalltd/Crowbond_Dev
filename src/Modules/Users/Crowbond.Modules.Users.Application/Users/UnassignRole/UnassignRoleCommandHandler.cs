using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.Users.Application.Abstractions.Data;
using Crowbond.Modules.Users.Domain.Users;

namespace Crowbond.Modules.Users.Application.Users.UnassignRole;

internal sealed class UnassignRoleCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UnassignRoleCommand>
{
    public async Task<Result> Handle(UnassignRoleCommand request, CancellationToken cancellationToken)
    {
        User? user = await userRepository.GetAsync(request.UserId, cancellationToken);

        if (user is null)
        {
            return Result.Failure(UserErrors.NotFound(request.UserId));
        }

        Role? role = await userRepository.GetRoleAsync(request.RoleName, cancellationToken);

        if (role is null)
        {
            return Result.Failure(UserErrors.RoleNotFound(request.RoleName));
        }

        Result result = user.RemoveRole(role);

        if (result.IsFailure)
        {
            return result;
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
