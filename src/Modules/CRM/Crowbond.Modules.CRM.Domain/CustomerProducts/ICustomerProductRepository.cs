namespace Crowbond.Modules.CRM.Domain.CustomerProducts;

public interface ICustomerProductRepository
{
    Task<CustomerProduct?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    void Insert(CustomerProduct customerProduct);
}
