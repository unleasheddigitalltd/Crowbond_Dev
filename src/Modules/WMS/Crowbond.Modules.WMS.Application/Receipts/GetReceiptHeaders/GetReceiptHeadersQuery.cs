using Crowbond.Common.Application.Messaging;
using Crowbond.Modules.WMS.Application.Receipts.GetReceipts.Dtos;

namespace Crowbond.Modules.WMS.Application.Receipts.GetReceipts;
public sealed record GetReceiptHeadersQuery(string Search, string Sort, string Order, int Page, int Size) : IQuery<ReceiptHeadersResponse>;
