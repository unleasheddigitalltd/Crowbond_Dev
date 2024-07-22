using Crowbond.Modules.CRM.Domain.CustomerProductPrices;
using Crowbond.Modules.CRM.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Crowbond.Modules.CRM.Infrastructure.CustomerProductPrices;

internal sealed class CustomerProductPriceRepository(CrmDbContext context) : ICustomerProductPriceRepository
{
    public async Task<CustomerProductPrice?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.CustomerProductPrices.SingleOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public void Insert(CustomerProductPrice customerProductPrice)
    {
        context.CustomerProductPrices.Add(customerProductPrice);
    }
}
