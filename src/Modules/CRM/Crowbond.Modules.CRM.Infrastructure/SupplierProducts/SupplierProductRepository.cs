using Crowbond.Modules.CRM.Domain.SupplierProducts;
using Crowbond.Modules.CRM.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Crowbond.Modules.CRM.Infrastructure.SupplierProducts;

internal sealed class SupplierProductRepository(CrmDbContext context) : ISupplierProductRepository
{
    public async Task<IEnumerable<SupplierProduct>> GetForSupplierAsync(Guid supplierId, CancellationToken cancellationToken = default)
    {
        return await context.SupplierProducts.Where(sp => sp.SupplierId == supplierId).ToListAsync(cancellationToken);
    }

    public void Insert(SupplierProduct supplierProduct)
    {
        context.SupplierProducts.Add(supplierProduct);
    }

    public void Remove(SupplierProduct supplierProduct)
    {
        context.SupplierProducts.Remove(supplierProduct);
    }
}
