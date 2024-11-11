using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.Compliances.SubmitCompliance;

public sealed record SubmitComplianceCommand(Guid DriverId, decimal Temprature) : ICommand<bool>;
