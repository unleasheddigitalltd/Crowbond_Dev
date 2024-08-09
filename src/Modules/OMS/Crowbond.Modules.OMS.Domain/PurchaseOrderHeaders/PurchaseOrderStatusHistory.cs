using Crowbond.Common.Domain;

namespace Crowbond.Modules.OMS.Domain.PurchaseOrderHeaders;

public sealed class PurchaseOrderStatusHistory : Entity
{
    private PurchaseOrderStatusHistory()
    {        
    }

    public Guid Id { get; private set; }

    public Guid PurchaseOrderHeaderId { get; private set; }

    public PurchaseOrderStatus Status { get; private set; }

    public DateTime ChangedAt { get; private set; }

    public Guid ChangedBy { get; private set; }

    public static PurchaseOrderStatusHistory Create(
        Guid purchaseOrderHeaderId,
        PurchaseOrderStatus status,
        DateTime changedAt,
        Guid changedBy)
    {
        var purchaseOrderStatusHistory = new PurchaseOrderStatusHistory
        {
            Id = Guid.NewGuid(),
            PurchaseOrderHeaderId = purchaseOrderHeaderId,
            Status = status,
            ChangedAt = changedAt,
            ChangedBy = changedBy
        };

        return purchaseOrderStatusHistory;
    }
}
