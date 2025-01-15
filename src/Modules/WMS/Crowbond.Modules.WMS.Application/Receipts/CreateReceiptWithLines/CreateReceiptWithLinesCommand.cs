using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Receipts.CreateReceiptWithLines;

public sealed record CreateReceiptWithLinesCommand(ReceiptRequest Receipt) : ICommand<Guid>;
