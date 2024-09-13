using Crowbond.Modules.CRM.Domain.CustomerProducts;
using Crowbond.Modules.CRM.Infrastructure.Database;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Crowbond.Modules.CRM.Infrastructure.CustomerProducts;

internal sealed class CustomerProductRepository(CrmDbContext context) : ICustomerProductRepository
{
    public async Task<IEnumerable<CustomerProduct>> GetForCustomerAsync(Guid customerId, CancellationToken cancellationToken = default)
    {
        return await context.CustomerProducts.Where(c => c.CustomerId == customerId).ToListAsync(cancellationToken);
    }

    public void Insert(CustomerProduct customerProduct)
    {
        context.CustomerProducts.Add(customerProduct);
    }

    public void Remove(CustomerProduct customerProduct)
    {
        context.CustomerProducts.Remove(customerProduct);
    }

    public void InsertPriceHistory(CustomerProductPriceHistory priceHistory)
    {
        context.CustomerProductPriceHistory.Add(priceHistory);
    }    
}
