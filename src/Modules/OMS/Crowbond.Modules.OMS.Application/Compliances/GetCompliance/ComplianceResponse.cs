namespace Crowbond.Modules.OMS.Application.Compliances.GetCompliance;

public sealed record ComplianceResponse(
    Guid Id,
    string VehicleRegn,
    string DriverFirstName,
    string DriverLastName,
    string FormNo,
    DateOnly FormDate)
{
    public List<ComplianceLineResponse> ComplianceLines { get; } = [];
}

