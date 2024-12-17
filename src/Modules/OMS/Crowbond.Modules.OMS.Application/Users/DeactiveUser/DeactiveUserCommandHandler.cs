using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Application.Abstractions.Data;
using Crowbond.Modules.OMS.Domain.Users;

namespace Crowbond.Modules.OMS.Application.Users.DeactiveUser;

internal sealed class DeactiveUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<DeactiveUserCommand>
{
    public async Task<Result> Handle(DeactiveUserCommand request, CancellationToken cancellationToken)
    {
        User? user = await userRepository.GetAsync(request.UserId, cancellationToken);

        if (user is null)
        {
            return Result.Failure(UserErrors.NotFound(request.UserId));
        }

        user.Deactivate();

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
