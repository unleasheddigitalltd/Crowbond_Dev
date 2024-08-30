using Crowbond.Modules.WMS.Domain.Sequences;

namespace Crowbond.Modules.WMS.Domain.Receipts;

public interface IReceiptRepository
{
    Task<ReceiptHeader?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IEnumerable<ReceiptLine>> GetLinesAsync(Guid id, CancellationToken cancellationToken = default);

    Task<ReceiptHeader?> GetByPurchaseOrderIdAsync(Guid purchaseOrderId, CancellationToken cancellationToken = default);

    void AddLines(IEnumerable<ReceiptLine> purchaseOrderLines);

    Task<Sequence?> GetSequenceAsync(CancellationToken cancellationToken = default);

    void Insert(ReceiptHeader purchaseorderheader);

}
