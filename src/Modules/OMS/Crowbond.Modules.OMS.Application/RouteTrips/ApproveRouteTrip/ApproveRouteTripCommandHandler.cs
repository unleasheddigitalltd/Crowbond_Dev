using Crowbond.Common.Application.Clock;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Application.Abstractions.Data;
using Crowbond.Modules.OMS.Domain.RouteTrips;

namespace Crowbond.Modules.OMS.Application.RouteTrips.ApproveRouteTrip;

internal sealed class ApproveRouteTripCommandHandler(
    IRouteTripRepository routeTripRepository,
    IDateTimeProvider dateTimeProvider,
    IUnitOfWork unitOfWork)
    : ICommandHandler<ApproveRouteTripCommand>
{
    public async Task<Result> Handle(ApproveRouteTripCommand request, CancellationToken cancellationToken)
    {
        RouteTrip? routeTrip = await routeTripRepository.GetAsync(request.RouteTripId, cancellationToken);

        if (routeTrip == null)
        {
            return Result.Failure(RouteTripErrors.NotFound(request.RouteTripId));
        }

        // check the route trip is not expired.
        if (routeTrip.Date < DateOnly.FromDateTime(dateTimeProvider.UtcNow))
        {
            return Result.Failure(RouteTripErrors.Expired(request.RouteTripId));
        }

        Result result = routeTrip.Approve();

        if (result.IsFailure)
        {
            return result;
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
