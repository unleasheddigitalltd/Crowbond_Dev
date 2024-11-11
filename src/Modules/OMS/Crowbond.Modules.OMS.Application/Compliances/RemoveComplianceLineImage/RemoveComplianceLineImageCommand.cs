using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.Compliances.RemoveComplianceLineImage;

public sealed record RemoveComplianceLineImageCommand(Guid ComplianceLineId, string ImageName) : ICommand;
