using Crowbond.Common.Application.Clock;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Application.Abstractions.Data;
using Crowbond.Modules.OMS.Domain.Drivers;
using Crowbond.Modules.OMS.Domain.RouteTripLogs;
using Crowbond.Modules.OMS.Domain.RouteTrips;

namespace Crowbond.Modules.OMS.Application.RouteTrips.LogOffRouteTrip;

internal sealed class LogOffRouteTripCommandHandler(
    IDriverRepository driverRepository,
    IRouteTripRepository routeTripRepository,
    IRouteTripLogRepository routeTripLogRepository,
    IDateTimeProvider dateTimeProvider,
    IUnitOfWork unitOfWork)
    : ICommandHandler<LogOffRouteTripCommand>
{
    public async Task<Result> Handle(LogOffRouteTripCommand request, CancellationToken cancellationToken)
    {
        Driver? driver = await driverRepository.GetAsync(request.DriverId, cancellationToken);
        if (driver == null)
        {
            return Result.Failure(DriverErrors.NotFound(request.DriverId));
        }

        RouteTrip? routeTrip = await routeTripRepository.GetAsync(request.RouteTripId, cancellationToken);
        if (routeTrip == null)
        {
            return Result.Failure(RouteTripErrors.NotFound(request.RouteTripId));
        }

        IEnumerable<RouteTripLog> routeTripLogs = await routeTripLogRepository.GetForRouteTripAsync(routeTrip, cancellationToken);

        RouteTripLog? routeTripLog = routeTripLogs.SingleOrDefault(t => t.DriverId == request.DriverId && t.LoggedOffTime == null);
        if (routeTripLog == null)
        {
            return Result.Failure(RouteTripLogErrors.NotFound(routeTrip.Id, driver.Id));
        }

        Result result = routeTripLog.LogOff(dateTimeProvider.UtcNow);

        if (result.IsFailure)
        {
            return Result.Failure(result.Error);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
