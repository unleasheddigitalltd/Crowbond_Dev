namespace Crowbond.Modules.CRM.Domain.CustomerProducts;

public interface ICustomerProductRepository
{
    Task<CustomerProduct?> GetByCustomerAndProductAsync(Guid customerId, Guid productId, CancellationToken cancellationToken = default);

    void Insert(CustomerProduct customerProduct);

    void Remove(CustomerProduct customerProduct);

    void InsertPriceHistory(CustomerProductPriceHistory priceHistory);
}
