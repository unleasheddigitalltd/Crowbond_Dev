using Crowbond.Modules.OMS.Domain.Sequences;

namespace Crowbond.Modules.OMS.Domain.Orders;

public interface IOrderRepository
{
    Task<OrderHeader?> GetAsync(Guid id, CancellationToken cancellationToken = default);
    
    Task<OrderHeader?> GetWithImagesAsync(Guid id, CancellationToken cancellationToken = default);

    Task<OrderLine?> GetLineAsync(Guid id, CancellationToken cancellationToken= default);

    Task<Sequence?> GetSequenceAsync(CancellationToken cancellationToken = default);

    Task<OrderLineRejectReason?> GetLineRejectReasonAsync(Guid id, CancellationToken cancellationToken = default);

    void Insert(OrderHeader orderHeader);

    void AddLine(OrderLine line);

    void AddLineReject(OrderLineReject lineReject);

    void AddDelivery(OrderDelivery delivery);

    void AddDeliveryImage(OrderDeliveryImage image);

    void Remove(OrderHeader orderHeader);

    void RemoveLine(OrderLine line);

    void RemoveDeliveryImage(OrderDeliveryImage image);
}
