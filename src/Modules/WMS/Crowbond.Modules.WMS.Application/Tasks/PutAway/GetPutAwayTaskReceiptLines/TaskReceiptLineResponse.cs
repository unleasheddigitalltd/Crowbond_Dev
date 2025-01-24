namespace Crowbond.Modules.WMS.Application.Tasks.PutAway.GetPutAwayTaskReceiptLines;

public sealed record TaskReceiptLineResponse(
    Guid ReceiptLineId,
    string ProductName,
    decimal ReceivedQty,
    decimal StoredQty,
    decimal MissedQty,
    bool IsStored);
