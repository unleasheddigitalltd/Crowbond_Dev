namespace Crowbond.Modules.Products.Domain.Products;

public interface IProductRepository
{
    Task<Product?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    Task<FilterType?> GetFilterTypeAsync(string name, CancellationToken cancellationToken = default);

    Task<InventoryType?> GetInventoryTypeAsync(string name, CancellationToken cancellationToken = default);

    Task<UnitOfMeasure?> GetUnitOfMeasureAsync(string name, CancellationToken cancellationToken = default);

    void Insert(Product product);
}
