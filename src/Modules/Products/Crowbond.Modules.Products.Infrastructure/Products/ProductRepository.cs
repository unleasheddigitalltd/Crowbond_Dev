using Crowbond.Modules.Products.Domain.Products;
using Crowbond.Modules.Products.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Crowbond.Modules.Products.Infrastructure.Products;

internal sealed class ProductRepository(ProductsDbContext context) : IProductRepository
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
