using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Receipts.CancelReceipt;

public sealed record CancelReceiptCommand(Guid PurchaseOrderId) : ICommand;
