using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.PublicApi;
using Crowbond.Modules.Users.Application.Abstractions.Data;
using Crowbond.Modules.Users.Domain.Users;
using Crowbond.Modules.WMS.PublicApi;

namespace Crowbond.Modules.Users.Application.Users.UnassignRole;

internal sealed class UnassignRoleCommandHandler(
    IDriverApi driverApi,
    IWarehouseOperatorApi warehouseOperatorApi,
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

        if (role.Name == Role.Driver.Name)
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

        if (role.Name == Role.WarehouseOperator.Name)
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

        Result result = user.RemoveRole(role);

        if (result.IsFailure)
        {
            return result;
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
