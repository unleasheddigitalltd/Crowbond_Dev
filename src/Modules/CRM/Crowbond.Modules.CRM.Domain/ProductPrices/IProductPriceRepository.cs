namespace Crowbond.Modules.CRM.Domain.ProductPrices;

public interface IProductPriceRepository
{
    Task<ProductPrice?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IEnumerable<ProductPrice>> GetForPriceTierAsync(Guid priceTierId, CancellationToken cancellationToken = default);
    
    Task<IEnumerable<ProductPrice>> GetForProductAsync(Guid productId, CancellationToken cancellationToken = default);

    void Insert(ProductPrice price);

    void Remove(ProductPrice price);
}
