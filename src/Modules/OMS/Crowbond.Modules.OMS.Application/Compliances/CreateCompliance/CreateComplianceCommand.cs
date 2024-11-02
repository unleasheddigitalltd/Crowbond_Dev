using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.Compliances.CreateCompliance;

public sealed record CreateComplianceCommand(Guid DriverId, Guid VehicleId): ICommand;
