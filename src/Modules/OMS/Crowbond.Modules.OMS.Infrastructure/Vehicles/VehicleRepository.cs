using Crowbond.Modules.OMS.Domain.Vehicles;
using Crowbond.Modules.OMS.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Crowbond.Modules.OMS.Infrastructure.Vehicles;
internal sealed class VehicleRepository(OmsDbContext context) : IVehicleRepository
{
    public async Task<Vehicle?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Vehicles.SingleOrDefaultAsync(v => v.Id == id, cancellationToken);
    }

    public void Insert(Vehicle vehicle)
    {
        context.Vehicles.Add(vehicle);
    }
}
