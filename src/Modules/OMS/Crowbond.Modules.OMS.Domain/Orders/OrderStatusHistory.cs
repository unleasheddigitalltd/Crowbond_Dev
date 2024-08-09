using Crowbond.Common.Domain;

namespace Crowbond.Modules.OMS.Domain.Orders;

public sealed class OrderStatusHistory : Entity
{
    private OrderStatusHistory()
    {        
    }

    public Guid Id { get; private set; }

    public Guid OrderHeaderId { get; private set; }

    public OrderStatus Status { get; private set; }

    public DateTime ChangedAt { get; private set; }

    public Guid ChangedBy { get; private set; }

    public static OrderStatusHistory Create(
        Guid orderHeaderId,
        OrderStatus status,
        DateTime changedAt,
        Guid changedBy)
    {
        var orderStatusHistory = new OrderStatusHistory
        {
            Id = Guid.NewGuid(),
            OrderHeaderId = orderHeaderId,
            Status = status,
            ChangedAt = changedAt,
            ChangedBy = changedBy
        };

        return orderStatusHistory;
    }
}
