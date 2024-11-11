using Crowbond.Common.Application.Clock;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Application.Abstractions.Data;
using Crowbond.Modules.OMS.Domain.Compliances;
using Crowbond.Modules.OMS.Domain.Drivers;
using Crowbond.Modules.OMS.Domain.RouteTripLogs;
using Crowbond.Modules.OMS.Domain.Sequences;
using Crowbond.Modules.OMS.Domain.Vehicles;

namespace Crowbond.Modules.OMS.Application.Compliances.CreateCompliance;

internal sealed class CreateComplianceCommandHandler(
    IDriverRepository driverRepository,
    IVehicleRepository vehicleRepository,
    IRouteTripLogRepository routeTripLogRepository,
    IComplianceRepository complianceRepository,
    IDateTimeProvider dateTimeProvider,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateComplianceCommand>
{
    public async Task<Result> Handle(CreateComplianceCommand request, CancellationToken cancellationToken)
    {
        Driver? driver = await driverRepository.GetAsync(request.DriverId, cancellationToken);
        if (driver == null)
        {
            return Result.Failure(DriverErrors.NotFound(request.DriverId));
        }

        Vehicle? vehicle = await vehicleRepository.GetAsync(request.VehicleId, cancellationToken);
        if (vehicle == null)
        {
            return Result.Failure(VehicleErrors.NotFound(request.VehicleId));
        }    

        RouteTripLog? routeTripLog = await routeTripLogRepository.GetActiveByDateAndDriver(
            DateOnly.FromDateTime(dateTimeProvider.UtcNow),
            driver.Id,
            cancellationToken);

        if (routeTripLog is null)
        {
            return Result.Failure(RouteTripLogErrors.ActiveLogForDriverNotFound);
        }

        ComplianceHeader? unSubmittedCompliance = await complianceRepository.GetUnSubmittedByRouteTripLogAsync(routeTripLog.Id, cancellationToken);

        if (unSubmittedCompliance is not null)
        {
            return Result.Failure(ComplianceErrors.UnsubmittedFound);
        }

        Sequence? sequence = await complianceRepository.GetSequence(cancellationToken);

        if (sequence == null)
        {
            return Result.Failure(ComplianceErrors.SequenceNotFound);
        }

        var complianceHeader = ComplianceHeader.Create(
            routeTripLog.Id, 
            vehicle.Id, 
            sequence.GetNumber(), 
            DateOnly.FromDateTime(dateTimeProvider.UtcNow));

        IReadOnlyCollection<ComplianceQuestion> questions = await complianceRepository.GetActiveQuestionsAsync(cancellationToken);
        foreach (ComplianceQuestion question in questions)
        {
            complianceHeader.AddLine(question.Id);
        }

        complianceRepository.Insert(complianceHeader);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
