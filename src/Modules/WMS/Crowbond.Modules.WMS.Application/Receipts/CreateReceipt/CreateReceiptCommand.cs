using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Receipts.CreateReceipt;

public sealed record CreateReceiptCommand(ReceiptRequest Receipt) : ICommand<Guid>;
