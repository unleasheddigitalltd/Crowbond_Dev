namespace Crowbond.Modules.CRM.Domain.CustomerProducts;

public interface ICustomerProductRepository
{
    Task<IEnumerable<CustomerProduct>> GetForCustomerAsync(Guid customerId, CancellationToken cancellationToken = default);

    void Insert(CustomerProduct customerProduct);

    void Remove(CustomerProduct customerProduct);
}
