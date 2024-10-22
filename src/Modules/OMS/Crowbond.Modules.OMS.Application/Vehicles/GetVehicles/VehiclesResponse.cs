using Crowbond.Common.Application.Pagination;
using Crowbond.Modules.OMS.Domain.Vehicles;

namespace Crowbond.Modules.OMS.Application.Vehicles.GetVehicles;

public sealed class VehiclesResponse : PaginatedResponse<Vehicle>
{
    public VehiclesResponse(IReadOnlyCollection<Vehicle> vehicles, IPagination pagination)
        : base(vehicles, pagination)
    { }
}

public sealed record Vehicle()
{
    public Guid Id { get; }
    public string VehicleRegn { get; }
    public bool InUse { get; }
}
