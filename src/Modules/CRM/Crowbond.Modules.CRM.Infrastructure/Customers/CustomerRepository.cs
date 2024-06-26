using Crowbond.Modules.CRM.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Crowbond.Modules.CRM.Domain.Customers;

namespace Crowbond.Modules.CRM.Infrastructure.Customers;

internal sealed class CustomerRepository(CrmDbContext context) : ICustomerRepository
{
    public async Task<Customer?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Customers.SingleOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public void Insert(Customer customer)
    {
        context.Customers.Add(customer);
    }
}
