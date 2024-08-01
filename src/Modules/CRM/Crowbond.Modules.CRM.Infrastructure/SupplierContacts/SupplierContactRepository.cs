using Crowbond.Modules.CRM.Domain.SupplierContacts;
using Crowbond.Modules.CRM.Domain.Suppliers;
using Crowbond.Modules.CRM.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Crowbond.Modules.CRM.Infrastructure.SupplierContacts;

internal sealed class SupplierContactRepository(CrmDbContext context) : ISupplierContactRepository
{
    public async Task<SupplierContact?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.SupplierContacts.SingleOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<SupplierContact>> GetForCustomerAsync(Supplier supplier, CancellationToken cancellationToken = default)
    {
        return await context.SupplierContacts.Where(s => s.SupplierId == supplier.Id).ToListAsync(cancellationToken);
    }

    public void Insert(SupplierContact contact)
    {
        context.Add(contact);
    }

    public void InsertRange(IEnumerable<SupplierContact> contacts)
    {
        context.AddRange(contacts);
    }
}
