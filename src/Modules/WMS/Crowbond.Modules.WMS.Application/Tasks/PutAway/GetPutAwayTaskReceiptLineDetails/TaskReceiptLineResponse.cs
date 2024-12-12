namespace Crowbond.Modules.WMS.Application.Tasks.PutAway.GetPutAwayTaskReceiptLineDetails;

public sealed record TaskReceiptLineResponse(
    Guid Id,
    Guid TaskId,
    string ProductName,
    decimal ReceivedQty,
    decimal StoredQty,
    decimal MissedQty,
    string BatchNumber,
    DateOnly? SellByDate,
    DateOnly? UseByDate,
    Guid LocationId,
    bool IsStored);
