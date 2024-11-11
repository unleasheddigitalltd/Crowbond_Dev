using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Application.Abstractions.Data;
using Crowbond.Modules.OMS.Domain.Compliances;

namespace Crowbond.Modules.OMS.Application.Compliances.RemoveComplianceLineImage;

internal sealed class RemoveComplianceLineImageCommandHandler(
    IComplianceRepository complianceRepository,
    IComplianceFileAccess complianceFileAccess,
    IUnitOfWork unitOfWork)
    : ICommandHandler<RemoveComplianceLineImageCommand>
{
    public async Task<Result> Handle(RemoveComplianceLineImageCommand request, CancellationToken cancellationToken)
    {
        ComplianceLine? complianceLine = await complianceRepository.GetLineWithImagesAsync(request.ComplianceLineId, cancellationToken);

        if (complianceLine == null)
        {
            return Result.Failure(ComplianceErrors.LineNotFound(request.ComplianceLineId));
        }

        Result<ComplianceLineImage> result = complianceLine.Header.RemoveLineImage(complianceLine, request.ImageName);

        if (result.IsFailure)
        {
            return Result.Failure(result.Error);
        }

        complianceRepository.RemoveLineImage(result.Value);

        await complianceFileAccess.DeleteLineImageAsync(request.ImageName, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
