using Crowbond.Modules.CRM.Domain.PriceTiers;
using Crowbond.Modules.CRM.Domain.Products;

namespace Crowbond.Modules.CRM.Domain.ProductPrices;

public interface IProductPriceRepository
{
    Task<ProductPrice?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    void Insert(ProductPrice price);
}
