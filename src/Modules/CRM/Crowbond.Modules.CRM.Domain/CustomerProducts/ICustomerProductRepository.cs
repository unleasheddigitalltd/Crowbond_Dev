namespace Crowbond.Modules.CRM.Domain.CustomerProducts;

public interface ICustomerProductRepository
{
    Task<CustomerProduct?> GetByCustomerAndProductAsync(Guid customerId, Guid productId, CancellationToken cancellationToken = default);
    
    Task<CustomerProductBlacklist?> GetBlacklistByCustomerAndProductAsync(Guid customerId, Guid productId, CancellationToken cancellationToken = default);

    void Insert(CustomerProduct customerProduct);
    
    void InsertBlacklist(CustomerProductBlacklist customerProductBlacklist);

    void RemoveBlacklist(CustomerProductBlacklist customerProductBlacklist);

    void InsertPriceHistory(CustomerProductPriceHistory priceHistory);
}
