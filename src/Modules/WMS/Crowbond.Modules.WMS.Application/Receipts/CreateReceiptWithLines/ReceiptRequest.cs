﻿namespace Crowbond.Modules.WMS.Application.Receipts.CreateReceiptWithLines;

public sealed record ReceiptRequest(
    Guid PurchaseOrderId,
    string? PurchaseOrderNo,
    List<ReceiptRequest.ReceiptLineRequest> ReceiptLines)
{
    public sealed record ReceiptLineRequest(
        Guid ProductId,
        decimal QuantityReceived,
        decimal UnitPrice,
        string BatchNumber,
        DateOnly? SellByDate,
        DateOnly? UseByDate);
}
