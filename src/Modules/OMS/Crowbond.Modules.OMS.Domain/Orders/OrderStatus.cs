namespace Crowbond.Modules.OMS.Domain.Orders;
public enum OrderStatus
{
    Pending = 0,
    Accepted = 1,
    Processing = 2,
    Shipped = 3,
    Delivered = 4,
    Cancelled = 5,
}
