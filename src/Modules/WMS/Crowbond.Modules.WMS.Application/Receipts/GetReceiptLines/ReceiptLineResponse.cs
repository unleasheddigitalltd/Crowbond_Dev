namespace Crowbond.Modules.WMS.Application.Receipts.GetReceiptLines;

public sealed record ReceiptLineResponse(
    Guid Id,
    string ProductSku,
    string ProductName,
    string UnitOfMeasure,
    decimal QuantityReceived,
    decimal UnitPrice,
    DateTime? SellByDate,
    DateTime? UseByDate,
    string Batch);

