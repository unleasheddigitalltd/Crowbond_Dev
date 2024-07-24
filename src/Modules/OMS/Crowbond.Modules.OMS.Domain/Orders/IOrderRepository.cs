using Crowbond.Modules.OMS.Domain.Sequences;

namespace Crowbond.Modules.OMS.Domain.Orders;

public interface IOrderRepository
{
    Task<OrderHeader?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Sequence?> GetSequenceAsync(CancellationToken cancellationToken = default);

    void InsertOrderHeader(OrderHeader orderHeader);
}
