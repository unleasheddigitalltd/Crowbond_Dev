using Crowbond.Modules.CRM.Domain.Customers;

namespace Crowbond.Modules.CRM.Domain.CustomerContacts;

public interface ICustomerContactRepository
{
    Task<IEnumerable<CustomerContact>> GetForCustomerAsync(Customer customer, CancellationToken cancellationToken = default);

    void InsertRange(IEnumerable<CustomerContact> contacts);
}
