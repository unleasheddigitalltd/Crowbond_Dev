using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.Compliances.GetComplianceLineImages;

public sealed record GetComplianceLineImagesQuery(Guid ComplianceLineId): IQuery<IReadOnlyCollection<ComplianceLineImageResponse>>;
