using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Receipts.ReceiveReceipt;

public sealed record ReceiveReceiptCommand(Guid UserId, Guid ReceiptId) : ICommand;
