using Crowbond.Common.Domain;

namespace Crowbond.Modules.WMS.Domain.Receipts;

public sealed class ReceiptLine : Entity
{
    public ReceiptLine()
    {
    }

    public Guid Id { get; private set; }

    public Guid ReceiptHeaderId { get; private set; }

    public Guid ProductId { get; private set; }

    public decimal QuantityReceived { get; private set; }

    public decimal UnitPrice { get; private set; }

    public DateOnly? SellByDate { get; private set; }

    public DateOnly? UseByDate { get; private set; }

    public string BatchNumber { get; private set; }

    internal static ReceiptLine Create(
        Guid productId,
        decimal quantityReceived,
        decimal unitPrice)
    {
        var receiptLine = new ReceiptLine
        {
            Id = Guid.NewGuid(),
            ProductId = productId,
            QuantityReceived = quantityReceived,
            UnitPrice = unitPrice,
            BatchNumber = GenerateBatchNumber()
        };

        return receiptLine;
    }

    private static string GenerateBatchNumber()
    {
        string batchNumber = "";
        return batchNumber;
    }


}
