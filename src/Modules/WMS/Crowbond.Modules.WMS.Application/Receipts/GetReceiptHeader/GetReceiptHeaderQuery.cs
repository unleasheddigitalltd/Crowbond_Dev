using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Receipts.GetReceiptHeader;

public sealed record GetReceiptHeaderQuery(Guid ReceiptHeaderId) : IQuery<ReceiptResponse>;
