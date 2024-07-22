using Crowbond.Modules.CRM.Domain.CustomerContacts;
using Crowbond.Modules.CRM.Domain.Customers;
using Crowbond.Modules.CRM.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Crowbond.Modules.CRM.Infrastructure.CustomerContacts;

internal sealed class CustomerContactRepository(CrmDbContext context) : ICustomerContactRepository
{

    public async Task<IEnumerable<CustomerContact>> GetForCustomerAsync(Customer customer, CancellationToken cancellationToken = default)
    {
        return await context.CustomerContacts.Where(c => c.CustomerId == customer.Id).ToListAsync(cancellationToken);
    }

    public void InsertRange(IEnumerable<CustomerContact> contacts)
    {
        context.CustomerContacts.AddRange(contacts);
    }

}
