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

        RouteTripLog? routeTripLog = await routeTripLogRepository.GetActiveByDateAndDriver(
            DateOnly.FromDateTime(dateTimeProvider.UtcNow),
            driver.Id,
            cancellationToken);

        if (routeTripLog == null)
        {
            return Result.Failure(RouteTripLogErrors.ActiveLogForDriverNotFound);
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
