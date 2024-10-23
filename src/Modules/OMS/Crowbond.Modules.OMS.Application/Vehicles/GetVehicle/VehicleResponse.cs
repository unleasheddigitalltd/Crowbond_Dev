namespace Crowbond.Modules.OMS.Application.Vehicles.GetVehicle;

public sealed record VehicleResponse(Guid Id, string VehicleRegn, bool InUse);
