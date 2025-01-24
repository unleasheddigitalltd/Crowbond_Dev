using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Receipts.GetReceiptLine;

public sealed record GetReceiptLineQuery(Guid ReceiptLineId) : IQuery<ReceiptLineResponse>;
