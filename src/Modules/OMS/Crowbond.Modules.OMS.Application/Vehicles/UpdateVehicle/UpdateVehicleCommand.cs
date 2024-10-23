using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.Vehicles.UpdateVehicle;

public sealed record UpdateVehicleCommand(Guid VehicleId, string VehicleRegn) : ICommand;
