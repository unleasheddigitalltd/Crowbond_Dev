using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Receipts.UpdateReceiptLine;

public sealed record UpdateReceiptLineCommand(
    Guid ReceiptLineId,
    decimal QuantityReceived,
    DateOnly? UseByDate, DateOnly? SellByDate, string? Batch) : ICommand;
