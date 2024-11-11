namespace Crowbond.Modules.OMS.Application.Compliances.UpdateCompliance;
public sealed record ComplianceLineRequest(Guid ComplianceLineId, bool Response, string Description);
