﻿using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Application.Abstractions.Data;
using Crowbond.Modules.WMS.Domain.Users;

namespace Crowbond.Modules.WMS.Application.Users.ActivateUser;

internal sealed class ActivateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<ActivateUserCommand>
{
    public async Task<Result> Handle(ActivateUserCommand request, CancellationToken cancellationToken)
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
