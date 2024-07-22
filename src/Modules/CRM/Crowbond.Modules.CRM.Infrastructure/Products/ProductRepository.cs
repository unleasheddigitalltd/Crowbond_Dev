using Crowbond.Modules.CRM.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Crowbond.Modules.CRM.Domain.Products;

namespace Crowbond.Modules.CRM.Infrastructure.Products;

internal sealed class ProductRepository(CrmDbContext context) : IProductRepository
{
    public async Task<Product?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Products.SingleOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public void Insert(Product product)
    {
        context.Products.Add(product);
    }
}
