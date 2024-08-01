using Crowbond.Modules.CRM.Domain.Customers;

namespace Crowbond.Modules.CRM.Domain.CustomerOutlets;

public interface ICustomerOutletRepository
{
    Task<CustomerOutlet?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IEnumerable<CustomerOutlet>> GetForCustomerAsync(Customer customer, CancellationToken cancellationToken = default);

    void Insert(CustomerOutlet customerOutlet);

    void InsertRange(IEnumerable<CustomerOutlet> customerOutlets);

}
