using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Application.Abstractions.Data;
using Crowbond.Modules.WMS.Domain.Users;

namespace Crowbond.Modules.WMS.Application.Users.CreateUser;

internal sealed class CreateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<CreateUserCommand>
{
    public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = User.Create(request.UserId,
            request.Username,
            request.Email,
            request.FirstName,
            request.LastName,
            request.Mobile);

        userRepository.Insert(user);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
