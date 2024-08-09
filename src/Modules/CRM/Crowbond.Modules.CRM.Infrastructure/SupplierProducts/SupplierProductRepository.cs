using Crowbond.Modules.CRM.Domain.SupplierProducts;
using Crowbond.Modules.CRM.Domain.Suppliers;
using Crowbond.Modules.CRM.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Crowbond.Modules.CRM.Infrastructure.SupplierProducts;

internal sealed class SupplierProductRepository(CrmDbContext context) : ISupplierProductRepository
{
    public async Task<SupplierProduct?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.SupplierProducts.SingleOrDefaultAsync(sp => sp.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<SupplierProduct>> GetForSupplierAsync(Supplier supplier, CancellationToken cancellationToken = default)
    {
        return await context.SupplierProducts.Where(sp => sp.SupplierId == supplier.Id).ToListAsync(cancellationToken);
    }

    public void Insert(SupplierProduct supplierProduct)
    {
        context.SupplierProducts.Add(supplierProduct);
    }

    public void InsertRange(IEnumerable<SupplierProduct> supplierProducts)
    {
        context.SupplierProducts.AddRange(supplierProducts);
    }
}
