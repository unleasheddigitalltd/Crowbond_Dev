using Crowbond.Common.Application.Messaging;
using Crowbond.Modules.WMS.Domain.Locations;

namespace Crowbond.Modules.WMS.Application.Receipts.ReceiveReceipt;

public sealed record ReceiveReceiptCommand(Guid ReceiptId, Guid LocationId) : ICommand;
