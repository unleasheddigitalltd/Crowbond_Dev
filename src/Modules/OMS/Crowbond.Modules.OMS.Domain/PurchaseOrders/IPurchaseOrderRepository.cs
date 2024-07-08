namespace Crowbond.Modules.OMS.Domain.PurchaseOrders;

public interface IPurchaseOrderRepository
{
    Task<PurchaseOrderHeader?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    void Insert(PurchaseOrderHeader purchaseorderheader);
}
