using Crowbond.Modules.CRM.Domain.CustomerProducts;
using Crowbond.Modules.CRM.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Crowbond.Modules.CRM.Infrastructure.CustomerProducts;

internal sealed class CustomerProductRepository(CrmDbContext context) : ICustomerProductRepository
{
    public async Task<CustomerProduct?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.CustomerProducts.SingleOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public void Insert(CustomerProduct customerProduct)
    {
        context.CustomerProducts.Add(customerProduct);
    }
}
