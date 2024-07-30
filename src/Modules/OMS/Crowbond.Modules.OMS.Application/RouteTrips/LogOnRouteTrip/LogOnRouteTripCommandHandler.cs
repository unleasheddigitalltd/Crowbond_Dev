using Crowbond.Common.Application.Clock;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Application.Abstractions.Data;
using Crowbond.Modules.OMS.Domain.Drivers;
using Crowbond.Modules.OMS.Domain.RouteTripLogs;
using Crowbond.Modules.OMS.Domain.RouteTrips;

namespace Crowbond.Modules.OMS.Application.RouteTrips.LogOnRouteTrip;

internal sealed class LogOnRouteTripCommandHandler(
    IDriverRepository driverRepository,
    IRouteTripRepository routeTripRepository,
    IRouteTripLogRepository routeTripLogRepository,
    IDateTimeProvider dateTimeProvider,
    IUnitOfWork unitOfWork)
    : ICommandHandler<LogOnRouteTripCommand, Guid>
{
    public async Task<Result<Guid>> Handle(LogOnRouteTripCommand request, CancellationToken cancellationToken)
    {
        Driver? driver =  await driverRepository.GetAsync(request.DriverId, cancellationToken);
        if (driver == null)
        {
            return Result.Failure<Guid>(DriverErrors.NotFound(request.DriverId));
        }

        if (driver.VehicleRegn == null) 
        {
            return Result.Failure<Guid>(DriverErrors.VehicleRegnNotFound());
        }

        RouteTrip? routeTrip = await routeTripRepository.GetAsync(request.RouteTripId, cancellationToken);
        if (routeTrip == null)
        { 
            return Result.Failure<Guid>(RouteTripErrors.NotFound(request.RouteTripId));
        }

        IEnumerable<RouteTripLog> routeTripLogs = await routeTripLogRepository.GetForRouteTripAsync(routeTrip, cancellationToken);
        if (routeTripLogs.Any(t => t.LoggedOffTime == null))
        {
            return Result.Failure<Guid>(RouteTripLogErrors.Exists(routeTrip.Id));
        }

        Result<RouteTripLog> result = RouteTripLog.Create(routeTrip.Id, driver.Id, driver.VehicleRegn, dateTimeProvider.UtcNow);

        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }

        routeTripLogRepository.Insert(result.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return result.Value.Id;
    }
}
