namespace Crowbond.Modules.WMS.Domain.Receipts;

public interface IReceiptRepository
{
    Task<ReceiptLine?> GetReceiptLineAsync(Guid id, CancellationToken cancellationToken = default);
}
