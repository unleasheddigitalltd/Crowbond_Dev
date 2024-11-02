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
    : ICommandHandler<LogOnRouteTripCommand>
{
    public async Task<Result> Handle(LogOnRouteTripCommand request, CancellationToken cancellationToken)
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

        // check route trip is available.
        if (routeTrip.Status != RouteTripStatus.Available)
        {
            return Result.Failure(RouteTripErrors.NotAvailable(request.RouteTripId));
        }

        // check the route trip is not expired.
        if (routeTrip.Date != DateOnly.FromDateTime(dateTimeProvider.UtcNow))
        {
            return Result.Failure(RouteTripErrors.Expired(request.RouteTripId));
        }

        var currentDate = DateOnly.FromDateTime(dateTimeProvider.UtcNow);

        // Check if the route trip already has an active log by another driver
        RouteTripLog? conflictingRouteTripLog = await routeTripLogRepository
            .GetActiveByDateAndRouteTripExcludingDriver(currentDate, routeTrip.Id, driver.Id, cancellationToken);

        if (conflictingRouteTripLog != null)
        {
            Driver? existDriver = await driverRepository.GetAsync(conflictingRouteTripLog.DriverId, cancellationToken);
            if (existDriver == null)
            {
                return Result.Failure(DriverErrors.NotFound(conflictingRouteTripLog.DriverId));
            }
            return Result.Failure(RouteTripLogErrors.OtherLogAlreadyExistsForRouteTrip($"{existDriver.FirstName} {existDriver.LastName}"));
        }

        // Check if the driver already has an active log for another route trip
        RouteTripLog? conflictingDriverLog = await routeTripLogRepository
            .GetActiveByDateAndDriverExcludingRouteTrip(currentDate, routeTrip.Id, driver.Id, cancellationToken);

        if (conflictingDriverLog != null)
        {
            return Result.Failure(RouteTripLogErrors.OtherActiveLogAlreadyExistsForDriver(conflictingDriverLog.RouteTripId));
        }

        // Check if the driver already has an active log for this route trip
        RouteTripLog? existingLog = await routeTripLogRepository
            .GetActiveByDateAndDriverAndRouteTrip(currentDate, driver.Id, routeTrip.Id, cancellationToken);

        if (existingLog != null)
        {
            return Result.Failure(RouteTripLogErrors.AlreadyExists);
        }


        var routeTripLog = RouteTripLog.Create(routeTrip.Id, driver.Id, dateTimeProvider.UtcNow);

        routeTripLogRepository.Insert(routeTripLog);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
