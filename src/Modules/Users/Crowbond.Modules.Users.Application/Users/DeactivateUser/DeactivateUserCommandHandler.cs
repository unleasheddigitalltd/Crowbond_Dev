using System.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.PublicApi;
using Crowbond.Modules.Users.Application.Abstractions.Data;
using Crowbond.Modules.Users.Application.Abstractions.Identity;
using Crowbond.Modules.Users.Domain.Users;
using Crowbond.Modules.WMS.PublicApi;

namespace Crowbond.Modules.Users.Application.Users.DeactivateUser;

internal sealed class DeactivateUserCommandHandler(
    IDriverApi driverApi,
    IWarehouseOperatorApi warehouseOperatorApi,
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

        Result result = await identityProviderService.UpdateUserAsync(
            user.IdentityId,
            new UserModel(user.Username, user.Email, string.Empty, user.FirstName, user.LastName, user.Mobile, false),
            cancellationToken);

        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }

        if (user.Roles.Any(r => r.Name == Role.Driver.Name))
        {
            ActiveRouteTripResponse? driverActiveRouteTrip = await driverApi.GetDriverActiveRouteTripAsync(request.UserId, cancellationToken);

            if (driverActiveRouteTrip is null)
            {
                return Result.Failure(UserErrors.DriverActiveRouteTripApiSentNoValue);
            }

            if (driverActiveRouteTrip.ActiveRouteName is not null)
            {
                return Result.Failure(UserErrors.DriverAlreadyLoggedOn(driverActiveRouteTrip.ActiveRouteName));
            }
        }

        if (user.Roles.Any(r => r.Name == Role.WarehouseOperator.Name))
        {
            ActiveTaskAssignmentResponse? operatorActiveTask = await warehouseOperatorApi.GetWarehouseOperatorActiveTaskAssignmentAsync(request.UserId, cancellationToken);

            if (operatorActiveTask is null)
            {
                return Result.Failure(UserErrors.DriverActiveRouteTripApiSentNoValue);
            }

            if (operatorActiveTask.TaskNo is not null)
            {
                return Result.Failure(UserErrors.WarehouseOperatorAlreadyAssigned(operatorActiveTask.TaskNo));
            }
        }

        user.Deactivate();
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
