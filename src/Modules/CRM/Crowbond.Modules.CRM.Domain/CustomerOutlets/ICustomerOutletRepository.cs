using Crowbond.Modules.CRM.Domain.Customers;

namespace Crowbond.Modules.CRM.Domain.CustomerOutlets;

public interface ICustomerOutletRepository
{
    Task<IEnumerable<CustomerOutlet>> GetForCustomerAsync(Customer customer, CancellationToken cancellationToken = default);

    void InsertRange(IEnumerable<CustomerOutlet> customerOutlets);

}
