using Crowbond.Common.Application.Messaging;
using Microsoft.AspNetCore.Http;

namespace Crowbond.Modules.OMS.Application.Compliances.UploadComplianceLineImages;

public sealed record UploadComplianceLineImagesCommand(Guid ComplianceLineId, IFormFileCollection Images) : ICommand;
