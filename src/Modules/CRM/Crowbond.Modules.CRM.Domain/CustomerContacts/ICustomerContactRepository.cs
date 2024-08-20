namespace Crowbond.Modules.CRM.Domain.CustomerContacts;

public interface ICustomerContactRepository
{
    Task<CustomerContact> GetAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IEnumerable<CustomerContact>> GetByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken = default);

    void Insert(CustomerContact customerContact);

    void InsertRange(IEnumerable<CustomerContact> contacts);
}
