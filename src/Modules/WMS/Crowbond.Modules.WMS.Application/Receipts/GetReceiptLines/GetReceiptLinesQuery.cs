using Crowbond.Common.Application.Messaging;
using Crowbond.Modules.WMS.Application.Receipts.GetReceiptLines.Dtos;

namespace Crowbond.Modules.WMS.Application.Receipts.GetReceiptLines;

public sealed record GetReceiptLinesQuery(Guid ReceiptHeaderId) : IQuery<IReadOnlyCollection<ReceiptLineResponse>>;
