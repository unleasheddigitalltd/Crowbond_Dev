using Crowbond.Modules.CRM.Domain.Sequences;

namespace Crowbond.Modules.CRM.Domain.Customers;

public interface ICustomerRepository
{
    Task<Customer?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Sequence?> GetSequenceAsync(CancellationToken cancellationToken = default);

    void Insert(Customer customer);
}
