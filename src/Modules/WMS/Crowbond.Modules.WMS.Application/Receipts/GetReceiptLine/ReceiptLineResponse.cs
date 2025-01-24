namespace Crowbond.Modules.WMS.Application.Receipts.GetReceiptLine;

public sealed record ReceiptLineResponse(
    Guid Id,
    string ProductSku,
    string ProductName,
    string UnitOfMeasure,
    decimal ReceivedQty,
    decimal StoredQty,
    decimal UnitPrice,
    DateTime? SellByDate,
    DateTime? UseByDate,
    string Batch,
    bool IsStored);
