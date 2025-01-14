using Crowbond.Modules.OMS.Domain.Sequences;

namespace Crowbond.Modules.OMS.Domain.PurchaseOrders;

public interface IPurchaseOrderRepository
{
    Task<PurchaseOrderHeader?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    Task<PurchaseOrderLine?> GetLineAsync(Guid id, CancellationToken cancellationToken = default);
    
    Task<PurchaseOrderHeader?> GetDraftBySupplierIdAsync(Guid supplierId, CancellationToken cancellationToken = default);

    void AddLines(IEnumerable<PurchaseOrderLine> purchaseOrderLines);

    void AddLine(PurchaseOrderLine purchaseOrderLine);

    Task<Sequence?> GetSequenceAsync(CancellationToken cancellationToken = default);

    void Insert(PurchaseOrderHeader purchaseorderheader);

    void InsertHistory(PurchaseOrderStatusHistory orderStatusHistory);
}
