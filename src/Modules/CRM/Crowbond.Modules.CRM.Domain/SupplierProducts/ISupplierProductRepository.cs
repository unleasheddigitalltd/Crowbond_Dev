using Crowbond.Modules.CRM.Domain.Suppliers;

namespace Crowbond.Modules.CRM.Domain.SupplierProducts;

public interface ISupplierProductRepository
{
    Task<SupplierProduct?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IEnumerable<SupplierProduct>> GetForSupplierAsync(Supplier supplier, CancellationToken cancellationToken = default);

    void InsertRange(IEnumerable<SupplierProduct> supplierProducts);

    void Insert(SupplierProduct supplierProduct);
}
