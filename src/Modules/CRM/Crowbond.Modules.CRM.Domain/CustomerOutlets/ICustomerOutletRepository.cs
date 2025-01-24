namespace Crowbond.Modules.CRM.Domain.CustomerOutlets;

public interface ICustomerOutletRepository
{
    Task<CustomerOutlet?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IEnumerable<CustomerOutlet>> GetByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken = default);

    void Insert(CustomerOutlet customerOutlet);

    void InsertRoute(CustomerOutletRoute route);

    void Remove(CustomerOutlet customerOutlet);

    void RemoveRoutes(IEnumerable<CustomerOutletRoute> routes);
}
