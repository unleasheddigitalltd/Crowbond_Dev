using Crowbond.Common.Domain;

namespace Crowbond.Modules.WMS.Domain.Receipts;
public static class ReceiptErrors
{
    public static Error NotFound(Guid receiptId) =>
    Error.NotFound("Receipts.NotFound", $"The receipt header with the identifier {receiptId} was not found");

}
