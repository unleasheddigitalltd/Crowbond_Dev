namespace Crowbond.Modules.OMS.Domain.PurchaseOrderHeaders;

public interface IPurchaseOrderHeaderRepository
{
    Task<PurchaseOrderHeader?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    void Insert(PurchaseOrderHeader purchaseorderheader);
}
