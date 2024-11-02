using Crowbond.Common.Application.Clock;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Application.Abstractions.Data;
using Crowbond.Modules.OMS.Domain.Compliances;
using Crowbond.Modules.OMS.Domain.Drivers;
using Crowbond.Modules.OMS.Domain.RouteTripLogs;

namespace Crowbond.Modules.OMS.Application.Compliances.UpdateCompliance;

internal sealed class UpdateComplianceCommandHandler(
    IDriverRepository driverRepository,
    IRouteTripLogRepository routeTripLogRepository,
    IComplianceRepository complianceRepository,
    IDateTimeProvider dateTimeProvider,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateComplianceCommand>
{
    public async Task<Result> Handle(UpdateComplianceCommand request, CancellationToken cancellationToken)
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

        foreach (ComplianceLineRequest line in request.Compliance.ComplianceLines)
        {
            Result result = compliance.UpdateLine(line.ComplianceLineId, line.Response, line.Description);

            if (result.IsFailure)
            {
                return result;
            }
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
