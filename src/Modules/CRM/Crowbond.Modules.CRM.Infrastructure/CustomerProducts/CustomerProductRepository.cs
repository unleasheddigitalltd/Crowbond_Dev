using Crowbond.Modules.CRM.Domain.CustomerProducts;
using Crowbond.Modules.CRM.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Crowbond.Modules.CRM.Infrastructure.CustomerProducts;

internal sealed class CustomerProductRepository(CrmDbContext context) : ICustomerProductRepository
{
    public async Task<CustomerProduct?> GetByCustomerAndProductAsync(Guid customerId, Guid productId, CancellationToken cancellationToken = default)
    {
        return await context.CustomerProducts.SingleOrDefaultAsync(c => c.CustomerId == customerId && c.ProductId == productId, cancellationToken);
    }

    public async Task<CustomerProductBlacklist?> GetBlacklistByCustomerAndProductAsync(Guid customerId, Guid productId, CancellationToken cancellationToken = default)
    {
        return await context.CustomerProductBlackList.SingleOrDefaultAsync(c => c.CustomerId == customerId && c.ProductId == productId, cancellationToken);
    }

    public void Insert(CustomerProduct customerProduct)
    {
        context.CustomerProducts.Add(customerProduct);
    }

    public void InsertBlacklist(CustomerProductBlacklist customerProductBlacklist)
    {
        context.CustomerProductBlackList.Add(customerProductBlacklist);
    }

    public void RemoveBlacklist(CustomerProductBlacklist customerProductBlacklist)
    {
        context.CustomerProductBlackList.Remove(customerProductBlacklist);
    }

    public void InsertPriceHistory(CustomerProductPriceHistory priceHistory)
    {
        context.CustomerProductPriceHistory.Add(priceHistory);
    }
}
