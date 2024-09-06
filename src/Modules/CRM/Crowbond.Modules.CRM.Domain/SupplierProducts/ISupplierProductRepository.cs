namespace Crowbond.Modules.CRM.Domain.SupplierProducts;

public interface ISupplierProductRepository
{
    Task<IEnumerable<SupplierProduct>> GetForSupplierAsync(Guid supplierId, CancellationToken cancellationToken = default);

    void Insert(SupplierProduct supplierProduct);

    void Remove(SupplierProduct supplierProduct);
}
