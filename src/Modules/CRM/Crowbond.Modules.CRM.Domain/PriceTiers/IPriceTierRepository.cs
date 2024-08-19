namespace Crowbond.Modules.CRM.Domain.PriceTiers;

public interface IPriceTierRepository
{
    Task<IEnumerable<PriceTier>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<PriceTier?> GetAsync(Guid id, CancellationToken cancellationToken = default);
}
