namespace Crowbond.Modules.OMS.Domain.Vehicles;

public interface IVehicleRepository
{
    Task<Vehicle?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    void Insert(Vehicle vehicle);
}
