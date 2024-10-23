using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.Vehicles.CreateVehicle;

public sealed record CreateVehicleCommand(string VehicleRegn): ICommand<Guid>;
