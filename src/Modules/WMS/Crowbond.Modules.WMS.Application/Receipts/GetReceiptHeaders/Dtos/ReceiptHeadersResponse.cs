namespace Crowbond.Modules.WMS.Application.Receipts.GetReceipts.Dtos;

public sealed record ReceiptHeadersResponse(
    IReadOnlyCollection<ReceiptHeader> ReceiptHeaders,

    Pagination Pagination
    );
