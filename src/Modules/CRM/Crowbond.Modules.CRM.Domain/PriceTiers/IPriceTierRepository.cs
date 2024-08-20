namespace Crowbond.Modules.CRM.Domain.PriceTiers;

public interface IPriceTierRepository
{

    Task<PriceTier?> GetAsync(Guid id, CancellationToken cancellationToken = default);
}
