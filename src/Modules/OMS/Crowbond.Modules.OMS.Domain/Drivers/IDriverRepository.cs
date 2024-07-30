namespace Crowbond.Modules.OMS.Domain.Drivers;

public interface IDriverRepository
{
    Task<Driver?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    void Insert(Driver driver);
}
