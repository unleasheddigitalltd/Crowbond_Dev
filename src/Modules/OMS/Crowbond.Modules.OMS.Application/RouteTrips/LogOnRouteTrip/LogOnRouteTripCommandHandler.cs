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

        RouteTripLog? activeLog = await routeTripLogRepository.GetActiveByRouteTripIdAsync(routeTrip.Id, cancellationToken);

        // check if there is already an active log.
        if (activeLog != null)
        {
            // log off current log if it has expired.
            if (activeLog.LoggedOnTime.Date != dateTimeProvider.UtcNow.Date)
            {
                Result logOffResult = activeLog.LogOff(dateTimeProvider.UtcNow);

                if (logOffResult.IsFailure)
                {
                    return Result.Failure(logOffResult.Error);
                }
            }
            else
            {
                if (activeLog.DriverId == request.DriverId)
                {
                    return Result.Failure(RouteTripLogErrors.AlreadyExists);
                }
                else
                {
                    Driver? existDriver = await driverRepository.GetAsync(activeLog.DriverId, cancellationToken);
                    if (existDriver == null)
                    {
                        return Result.Failure(DriverErrors.NotFound(activeLog.DriverId));
                    }

                    return Result.Failure(RouteTripLogErrors.ActiveLogExists($"{existDriver.FirstName} {existDriver.LastName}"));
                }
            }
        }

        var routeTripLog = RouteTripLog.Create(routeTrip.Id, driver.Id, dateTimeProvider.UtcNow);

        routeTripLogRepository.Insert(routeTripLog);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
