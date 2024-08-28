using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.ReceiveReceipt;

public sealed record ReceiveReceiptCommand(Guid UserId, Guid ReceiptId): ICommand;
