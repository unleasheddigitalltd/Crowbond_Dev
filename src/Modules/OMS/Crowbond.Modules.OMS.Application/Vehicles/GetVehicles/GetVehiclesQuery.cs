using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.Vehicles.GetVehicles;

public sealed record GetVehiclesQuery(string Search, string Sort, string Order, int Page, int Size) : IQuery<VehiclesResponse>;
