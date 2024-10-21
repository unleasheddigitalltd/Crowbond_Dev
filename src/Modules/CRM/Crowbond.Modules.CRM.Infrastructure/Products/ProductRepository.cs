using Crowbond.Modules.CRM.Domain.Products;
using Crowbond.Modules.CRM.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Crowbond.Modules.CRM.Infrastructure.Products;

internal sealed class ProductRepository(CrmDbContext context) : IProductRepository
{
    public async Task<Product?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Products.SingleOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<Category?> GetCategoryAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Categories.SingleOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<Brand?> GetBrandAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Brands.SingleOrDefaultAsync(b => b.Id == id, cancellationToken);
    }

    public async Task<ProductGroup?> GetProductGroupAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.ProductGroups.SingleOrDefaultAsync(pg => pg.Id == id, cancellationToken);
    }

    public void Insert(Product product)
    {
        context.Products.Add(product);
    }

    public void InsertCategory(Category category)
    {
        context.Categories.Add(category);
    }

    public void InsertBrand(Brand brand)
    {
        context.Brands.Add(brand);
    }

    public void InsertProductGroup(ProductGroup productGroup)
    {
        context.ProductGroups.Add(productGroup);
    }
}
