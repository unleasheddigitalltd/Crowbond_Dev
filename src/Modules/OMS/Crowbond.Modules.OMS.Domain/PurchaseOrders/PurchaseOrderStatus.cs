namespace Crowbond.Modules.OMS.Domain.PurchaseOrders;
public enum PurchaseOrderStatus
{
    Pending = 0,
    PendingPayment = 1,
    Accepted = 2,
    Processing = 3,
    Shipped = 4,
    Delivered = 5,
    Cancelled = 6,
    PaymentSent = 7,
}
