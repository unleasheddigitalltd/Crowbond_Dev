using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.Vehicles.GetVehicle;

public sealed record GetVehicleQuery(Guid VehicleId): IQuery<VehicleResponse>;
