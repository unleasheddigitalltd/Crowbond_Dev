using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Receipts.GetReceiptHeaderDetails;

public sealed record GetReceiptHeaderDetailsQuery(Guid ReceiptHeaderId) : IQuery<ReceiptHeaderDetailsResponse>;
