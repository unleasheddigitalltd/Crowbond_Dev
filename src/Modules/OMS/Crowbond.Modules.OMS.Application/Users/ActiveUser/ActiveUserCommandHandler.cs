using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Application.Abstractions.Data;
using Crowbond.Modules.OMS.Domain.Users;

namespace Crowbond.Modules.OMS.Application.Users.ActiveUser;

internal sealed class ActiveUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<ActiveUserCommand>
{
    public async Task<Result> Handle(ActiveUserCommand request, CancellationToken cancellationToken)
    {
        User? user = await userRepository.GetAsync(request.UserId, cancellationToken);

        if (user is null)
        {
            return Result.Failure(UserErrors.NotFound(request.UserId));
        }

        user.Activate();

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
