using Crowbond.Modules.WMS.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Crowbond.Modules.WMS.Domain.Products;

namespace Crowbond.Modules.WMS.Infrastructure.Products;

internal sealed class ProductRepository(WmsDbContext context) : IProductRepository
{
    public async Task<Product?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Products.SingleOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<Category?> GetCategoryAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Categories.Include(c => c.ProductGroups).SingleOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<Brand?> GetBrandAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Brands.SingleOrDefaultAsync(b => b.Id == id, cancellationToken);
    }

    public async Task<FilterType?> GetFilterTypeAsync(string name, CancellationToken cancellationToken = default)
    {
        return await context.FilterTypes.SingleOrDefaultAsync(f => f.Name == name, cancellationToken);
    }

    public async Task<InventoryType?> GetInventoryTypeAsync(string name, CancellationToken cancellationToken = default)
    {
        return await context.InventoryTypes.SingleOrDefaultAsync(i => i.Name == name, cancellationToken);
    }

    public async Task<ProductGroup?> GetProductGroupAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.ProductGroups.Include(pg => pg.Brands).SingleOrDefaultAsync(pg => pg.Id == id, cancellationToken);
    }

    public async Task<UnitOfMeasure?> GetUnitOfMeasureAsync(string name, CancellationToken cancellationToken = default)
    {
        return await context.UnitOfMeasures.SingleOrDefaultAsync(u => u.Name == name, cancellationToken);
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
