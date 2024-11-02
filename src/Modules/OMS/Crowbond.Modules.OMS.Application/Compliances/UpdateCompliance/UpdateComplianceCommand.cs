using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.Compliances.UpdateCompliance;

public sealed record UpdateComplianceCommand(Guid DriverId, ComplianceRequest Compliance) : ICommand;
