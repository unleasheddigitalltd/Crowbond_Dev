using Crowbond.Modules.Products.Domain.Categories;
using Crowbond.Modules.Products.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Crowbond.Modules.Products.Infrastructure.Categories;

internal sealed class CategoryRepository(ProductsDbContext context) : ICategoryRepository
{
    public async Task<Category?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Categories.SingleOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public void Insert(Category category)
    {
        context.Categories.Add(category);
    }
}
