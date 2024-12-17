using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Application.Abstractions.Data;
using Crowbond.Modules.OMS.Domain.Users;

namespace Crowbond.Modules.OMS.Application.Users.UpdateUser;

internal sealed class UpdateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateUserCommand>
{
    public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        User? user = await userRepository.GetAsync(request.UserId, cancellationToken);

        if (user == null)
        {
            return Result.Failure(UserErrors.NotFound(request.UserId));
        }

        user.Update(
            request.Username,
            request.Email,
            request.FirstName,
            request.LastName,
            request.Mobile);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
