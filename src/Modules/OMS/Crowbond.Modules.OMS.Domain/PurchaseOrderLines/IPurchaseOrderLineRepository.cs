namespace Crowbond.Modules.OMS.Domain.PurchaseOrderLines;

public interface IPurchaseOrderLineRepository
{
    Task<PurchaseOrderLine?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    void Insert(PurchaseOrderLine purchaseOrderLine);

    void InserRange(IEnumerable<PurchaseOrderLine> purchaseOrderLines);
}
