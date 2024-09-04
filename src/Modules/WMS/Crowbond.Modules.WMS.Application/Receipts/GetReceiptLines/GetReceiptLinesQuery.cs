using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Receipts.GetReceiptLines;

public sealed record GetReceiptLinesQuery(Guid ReceiptHeaderId) : IQuery<IReadOnlyCollection<ReceiptLineResponse>>;
