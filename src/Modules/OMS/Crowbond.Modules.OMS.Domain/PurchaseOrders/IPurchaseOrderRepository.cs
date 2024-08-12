namespace Crowbond.Modules.OMS.Domain.PurchaseOrders;

public interface IPurchaseOrderRepository
{
    Task<PurchaseOrderHeader?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    void AddLines(IEnumerable<PurchaseOrderLine> purchaseOrderLines);

    void Insert(PurchaseOrderHeader purchaseorderheader);

    void InsertHistory(PurchaseOrderStatusHistory orderStatusHistory);
}
