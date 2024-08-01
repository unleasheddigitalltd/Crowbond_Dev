using Crowbond.Modules.CRM.Domain.Customers;

namespace Crowbond.Modules.CRM.Domain.CustomerContacts;

public interface ICustomerContactRepository
{
    Task<CustomerContact> GetAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IEnumerable<CustomerContact>> GetForCustomerAsync(Customer customer, CancellationToken cancellationToken = default);

    void Insert(CustomerContact customerContact);

    void InsertRange(IEnumerable<CustomerContact> contacts);
}
