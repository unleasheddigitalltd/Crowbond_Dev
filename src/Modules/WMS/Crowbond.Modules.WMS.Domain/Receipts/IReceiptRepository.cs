namespace Crowbond.Modules.WMS.Domain.Receipts;

public interface IReceiptRepository
{
    Task<ReceiptLine?> GetReceiptLineAsync(Guid id, CancellationToken cancellationToken = default);

    Task<ReceiptHeader?> GetByPurchaseOrderIdAsync(Guid purchaseOrderId, CancellationToken cancellationToken = default);

    void InsertReceiptHeader(ReceiptHeader receiptHeader);

    void InsertReceiptLine(ReceiptLine receiptLine);

    void InsertRangeReceiptLines(IEnumerable<ReceiptLine> receiptLines);
}
