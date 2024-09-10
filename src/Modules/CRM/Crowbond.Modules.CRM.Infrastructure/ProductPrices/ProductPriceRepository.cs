using Crowbond.Modules.CRM.Domain.ProductPrices;
using Crowbond.Modules.CRM.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Crowbond.Modules.CRM.Infrastructure.ProductPrices;

public sealed class ProductPriceRepository(CrmDbContext context) : IProductPriceRepository
{
    public async Task<ProductPrice?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.ProductPrices.SingleOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<ProductPrice>> GetForPriceTierAsync(Guid priceTierId, CancellationToken cancellationToken = default)
    {
        return await context.ProductPrices.Where(p => p.PriceTierId ==  priceTierId).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<ProductPrice>> GetForProductAsync(Guid productId, CancellationToken cancellationToken = default)
    {
        return await context.ProductPrices.Where(p => p.ProductId == productId).ToListAsync(cancellationToken);
    }

    public void Insert(ProductPrice price)
    {
        context.ProductPrices.Add(price);
    }

    public void Remove(ProductPrice price)
    {
        context.ProductPrices.Remove(price);
    }
}
