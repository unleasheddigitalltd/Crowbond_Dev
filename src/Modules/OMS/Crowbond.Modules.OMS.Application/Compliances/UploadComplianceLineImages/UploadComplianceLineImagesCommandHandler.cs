using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Application.Abstractions.Data;
using Crowbond.Modules.OMS.Domain.Compliances;
using Microsoft.AspNetCore.Http;

namespace Crowbond.Modules.OMS.Application.Compliances.UploadComplianceLineImages;

internal sealed class UploadComplianceLineImagesCommandHandler(
    IComplianceRepository complianceRepository,
    IComplianceFileAccess complianceFileAccess,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UploadComplianceLineImagesCommand>
{
    public async Task<Result> Handle(UploadComplianceLineImagesCommand request, CancellationToken cancellationToken)
    {
        if (request.Images == null || request.Images.Count == 0)
        {
            return Result.Failure(ComplianceErrors.NoFilesUploaded);
        }

        var _allowedExtensions = new List<string> { ".jpg", ".jpeg", ".png", ".gif" };
        foreach (IFormFile image in request.Images)
        {
            if (image.Length > 1024 * 1024) // 1MB
            {
                return Result.Failure(ComplianceErrors.FileSizeExceeds);
            }

            string fileExtension = Path.GetExtension(image.FileName).ToLowerInvariant();
            if (!_allowedExtensions.Contains(fileExtension))
            {
                return Result.Failure(ComplianceErrors.InvalidFileType(_allowedExtensions));
            }
        }

        ComplianceLine? complianceLine = await complianceRepository.GetLineAsync(request.ComplianceLineId, cancellationToken);

        if (complianceLine == null)
        {
            return Result.Failure(ComplianceErrors.LineNotFound(request.ComplianceLineId));
        }

        List<string> imagesUrl = await complianceFileAccess.SaveLineImagesAsync(complianceLine.Header.FormNo, complianceLine.Header.LastImageSequence, request.Images, cancellationToken);

        foreach (string imageUrl in imagesUrl)
        {
            ComplianceLineImage complianceLineImage = complianceLine.Header.AddLineImage(complianceLine, imageUrl);
            complianceRepository.InsertLineImage(complianceLineImage);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
