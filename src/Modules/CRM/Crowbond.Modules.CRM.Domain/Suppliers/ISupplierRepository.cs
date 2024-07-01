namespace Crowbond.Modules.CRM.Domain.Suppliers;

public interface ISupplierRepository
{
    Task<Supplier?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    void Insert(Supplier supplier);
}
