using Crowbond.Common.Domain;

namespace Crowbond.Modules.OMS.Domain.PurchaseOrders;

public sealed class PurchaseOrderStatusHistory : Entity , ITrackable
{
    private PurchaseOrderStatusHistory()
    {
    }

    public Guid Id { get; private set; }

    public Guid PurchaseOrderHeaderId { get; private set; }

    public PurchaseOrderStatus Status { get; private set; }

    public DateTime ChangedAt { get; private set; }

    public Guid ChangedBy { get; set; }

    internal PurchaseOrderStatusHistory(PurchaseOrderStatus status, DateTime utcNow)
    {
        Id = Guid.NewGuid();
        Status = status;
        ChangedAt = utcNow;
    }
}
