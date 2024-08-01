using Crowbond.Modules.CRM.Domain.Sequences;

namespace Crowbond.Modules.CRM.Domain.Suppliers;

public interface ISupplierRepository
{
    Task<Supplier?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Sequence?> GetSequenceAsync(CancellationToken cancellationToken = default);

    void Insert(Supplier supplier);
}
