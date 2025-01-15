using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Receipts.RemoveReceiptLine;

public sealed record RemoveReceiptLineCommand(Guid ReceiptLineId) : ICommand;
