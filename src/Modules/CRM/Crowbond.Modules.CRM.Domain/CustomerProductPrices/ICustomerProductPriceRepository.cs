namespace Crowbond.Modules.CRM.Domain.CustomerProductPrices;

public interface ICustomerProductPriceRepository
{
    Task<CustomerProductPrice?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    void Insert(CustomerProductPrice customerProductPrice);
}
