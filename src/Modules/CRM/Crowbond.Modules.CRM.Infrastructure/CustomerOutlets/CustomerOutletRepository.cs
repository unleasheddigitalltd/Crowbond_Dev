using Crowbond.Modules.CRM.Domain.CustomerOutlets;
using Crowbond.Modules.CRM.Domain.Customers;
using Crowbond.Modules.CRM.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Crowbond.Modules.CRM.Infrastructure.CustomerOutlets;

internal sealed class CustomerOutletRepository(CrmDbContext context) : ICustomerOutletRepository
{

    public async Task<IEnumerable<CustomerOutlet>> GetForCustomerAsync(
        Customer customer,
        CancellationToken cancellationToken = default)
    {
        return await context.CustomerOutlets.Where(c => c.CustomerId == customer.Id).ToListAsync(cancellationToken);
    }

    public void InsertRange(IEnumerable<CustomerOutlet> customerOutlets)
    {
        context.CustomerOutlets.AddRange(customerOutlets);
    }

}
