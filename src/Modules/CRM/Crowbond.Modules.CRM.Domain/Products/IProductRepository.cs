namespace Crowbond.Modules.CRM.Domain.Products;

public interface IProductRepository
{
    Task<Product?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    void Insert(Product product);
}
