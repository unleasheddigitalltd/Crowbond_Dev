using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Receipts.AddReceiptLine;

public sealed record AddReceiptLineCommand(
    Guid ReceiptHeaderId,
    Guid ProductId, 
    decimal ReceivedQty,
    decimal UnitPrice) : ICommand<Guid>;
