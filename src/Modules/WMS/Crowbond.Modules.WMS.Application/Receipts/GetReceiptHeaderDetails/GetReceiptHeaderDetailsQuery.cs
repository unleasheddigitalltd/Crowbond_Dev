using Crowbond.Common.Application.Messaging;
using Crowbond.Modules.WMS.Application.Receipts.GetReceiptHeaderDetails.Dtos;

namespace Crowbond.Modules.WMS.Application.Receipts.GetReceiptHeaderDetails;

public sealed record GetReceiptHeaderDetailsQuery(Guid ReceiptHeaderId) : IQuery<ReceiptHeaderDetailsResponse>;
