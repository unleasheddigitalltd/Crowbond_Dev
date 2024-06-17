using Crowbond.Modules.WMS.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Crowbond.Modules.WMS.Domain.Categories;

namespace Crowbond.Modules.WMS.Infrastructure.Categories;

internal sealed class CategoryRepository(WmsDbContext context) : ICategoryRepository
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
