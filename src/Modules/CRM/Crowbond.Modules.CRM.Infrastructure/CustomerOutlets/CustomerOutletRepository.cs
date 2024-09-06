using Crowbond.Modules.CRM.Domain.CustomerOutlets;
using Crowbond.Modules.CRM.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Crowbond.Modules.CRM.Infrastructure.CustomerOutlets;

internal sealed class CustomerOutletRepository(CrmDbContext context) : ICustomerOutletRepository
{
    public async Task<CustomerOutlet?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.CustomerOutlets.SingleOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<CustomerOutlet>> GetByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken = default)
    {
        return await context.CustomerOutlets.Where(c => c.CustomerId == customerId).ToListAsync(cancellationToken);
    }

    public void Insert(CustomerOutlet customerOutlet)
    {
        context.CustomerOutlets.Add(customerOutlet);
    }

    public void Remove(CustomerOutlet customerOutlet)
    {
        context.CustomerOutlets.Remove(customerOutlet);
    }
}
