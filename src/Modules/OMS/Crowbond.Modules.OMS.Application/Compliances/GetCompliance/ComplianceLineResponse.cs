namespace Crowbond.Modules.OMS.Application.Compliances.GetCompliance;
public sealed record ComplianceLineResponse(
    Guid ComplianceLineId,
    Guid ComplianceId,
    string QuestionText,
    bool? Response,
    string? Description);
