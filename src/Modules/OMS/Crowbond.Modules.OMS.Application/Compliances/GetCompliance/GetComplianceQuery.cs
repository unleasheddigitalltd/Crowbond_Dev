using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.Compliances.GetCompliance;

public sealed record GetComplianceQuery(Guid DriverId) : IQuery<ComplianceResponse>;
