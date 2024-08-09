namespace Crowbond.Modules.OMS.Domain.PurchaseOrderHeaders;
public interface IPurchaseOrderStatusHistoryRepository
{
    Task<PurchaseOrderStatusHistory?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    void Insert(PurchaseOrderStatusHistory orderStatusHistory);
}
