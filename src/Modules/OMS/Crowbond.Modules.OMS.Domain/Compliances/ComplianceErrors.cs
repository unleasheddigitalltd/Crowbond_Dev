using Crowbond.Common.Domain;

namespace Crowbond.Modules.OMS.Domain.Compliances;

public static class ComplianceErrors
{
    public static Error NotFound(Guid complianceHeaderId) =>
        Error.NotFound("Compliances.NotFound", $"The compliance header with identifier {complianceHeaderId} was not found");
    
    public static Error LineNotFound(Guid complianceLineId) =>
        Error.NotFound("Compliances.NotFound", $"The compliance line with identifier {complianceLineId} was not found");

    public static Error ForDriverNotFound(Guid driverId) =>
        Error.NotFound("Compliances.ForDriverNotFound", $"No active compliance entry was found for the driver with identifier {driverId}");

    public readonly static Error UnsubmittedFound =
        Error.NotFound("Compliances.UnsubmittedFound", $"An unsubmitted compliance entry was found for this log");

    public readonly static Error UnsubmittedNotFound =
        Error.NotFound("Compliances.UnsubmittedNotFound", "The unsubmitted compliance entry was not found for this log");

    public readonly static Error SequenceNotFound =
        Error.NotFound("Sequence.NotFound", $"The sequence for the compliance type was not found");

    public readonly static Error NotAllQuestionsAnswered =
        Error.Problem("Compliance.NotAllQuestionsAnswered", "Not all required questions have been answered for this compliance check.");
}
