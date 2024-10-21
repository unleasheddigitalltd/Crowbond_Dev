namespace Crowbond.Modules.OMS.Domain.Orders;
public enum OrderStatus
{
    Pending = 0,
    StockReviewing = 1,
    Accepted = 2,
    Processing = 3,
    Shipped = 4,
    Delivered = 5,
    Cancelled = 6,
}
