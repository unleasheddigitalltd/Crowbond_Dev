using Crowbond.Modules.CRM.Domain.PriceTiers;
using Crowbond.Modules.CRM.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Crowbond.Modules.CRM.Infrastructure.PriceTiers;

public sealed class PriceTierRepository(CrmDbContext context) : IPriceTierRepository
{
    public async Task<IEnumerable<PriceTier>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.PriceTiers.ToListAsync(cancellationToken);
    }

    public async Task<PriceTier?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.PriceTiers.SingleOrDefaultAsync(pt => pt.Id == id, cancellationToken);
    }
}
