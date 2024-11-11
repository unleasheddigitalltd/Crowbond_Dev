using Crowbond.Common.Application.Clock;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Application.Abstractions.Data;
using Crowbond.Modules.OMS.Domain.Compliances;
using Crowbond.Modules.OMS.Domain.Drivers;
using Crowbond.Modules.OMS.Domain.RouteTripLogs;

namespace Crowbond.Modules.OMS.Application.Compliances.SubmitCompliance;

internal sealed class SubmitComplianceCommandHandler(
    IDriverRepository driverRepository,
    IComplianceRepository complianceRepository,
    IRouteTripLogRepository routeTripLogRepository,
    IDateTimeProvider dateTimeProvider,
    IUnitOfWork unitOfWork)
    : ICommandHandler<SubmitComplianceCommand, bool>
{
    public async Task<Result<bool>> Handle(SubmitComplianceCommand request, CancellationToken cancellationToken)
    {
        Driver? driver = await driverRepository.GetAsync(request.DriverId, cancellationToken);
        if (driver == null)
        {
            return Result.Failure<bool>(DriverErrors.NotFound(request.DriverId));
        }

        RouteTripLog? routeTripLog = await routeTripLogRepository.GetActiveByDateAndDriver(
            DateOnly.FromDateTime(dateTimeProvider.UtcNow),
            driver.Id,
            cancellationToken);

        if (routeTripLog == null)
        {
            return Result.Failure<bool>(RouteTripLogErrors.ActiveLogForDriverNotFound);
        }

        ComplianceHeader? compliance = await complianceRepository.GetUnSubmittedByRouteTripLogAsync(routeTripLog.Id, cancellationToken);

        if (compliance == null)
        {
            return Result.Failure<bool>(ComplianceErrors.UnsubmittedNotFound);
        }

        Result<bool> result = compliance.Submit(request.Temprature);

        if (result.IsFailure)
        {
            return Result.Failure<bool>(result.Error);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(result.Value);
    }
}
