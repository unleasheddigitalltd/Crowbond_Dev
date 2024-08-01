using Crowbond.Modules.CRM.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Crowbond.Modules.CRM.Domain.Suppliers;
using Crowbond.Modules.CRM.Domain.Sequences;

namespace Crowbond.Modules.CRM.Infrastructure.Suppliers;

internal sealed class SupplierRepository(CrmDbContext context) : ISupplierRepository
{
    public async Task<Supplier?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Suppliers.SingleOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public void Insert(Supplier supplier)
    {
        context.Suppliers.Add(supplier);
    }

    public async Task<Sequence?> GetSequenceAsync(CancellationToken cancellationToken = default)
    {
        return await context.Sequences.SingleOrDefaultAsync(s => s.Context == SequenceContext.Supplier, cancellationToken);
    }
}
