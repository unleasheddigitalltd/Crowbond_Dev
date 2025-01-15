using Crowbond.Common.Domain;

namespace Crowbond.Modules.WMS.Domain.Receipts;

public sealed class ReceiptHeader : Entity, IAuditable
{
    private readonly List<ReceiptLine> _lines = new();

    private ReceiptHeader()
    {
    }

    public Guid Id { get; private set; }

    public string ReceiptNo { get; private set; }

    public DateOnly? ReceivedDate { get; private set; }

    public Guid? PurchaseOrderId { get; private set; }

    public string? PurchaseOrderNo { get; private set; }

    public string? DeliveryNoteNumber { get; private set; }

    public Guid? LocationId { get; private set; }

    public ReceiptStatus Status { get; private set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedOnUtc { get; set; }

    public Guid? LastModifiedBy { get; set; }

    public DateTime? LastModifiedOnUtc { get; set; }

    public IReadOnlyCollection<ReceiptLine> Lines => _lines;


    public static ReceiptHeader Create(
        string receiptNo,
        Guid? purchaseOrderId,
        string? purchaseOrderNumber)
    {
        var receiptHeader = new ReceiptHeader
        {
            Id = Guid.NewGuid(),
            ReceiptNo = receiptNo,
            PurchaseOrderId = purchaseOrderId,
            PurchaseOrderNo = purchaseOrderNumber,
            Status = ReceiptStatus.Shipping
        };

        return receiptHeader;
    }

    public Result Receive(DateTime utcNow, Guid locationId)
    {
        if (Status != ReceiptStatus.Shipping)
        {
            return Result.Failure(ReceiptErrors.NotShipping);
        }

        ReceivedDate = DateOnly.FromDateTime(utcNow);
        LocationId = locationId;
        Status = ReceiptStatus.Received;

        return Result.Success();
    }

    public Result Cancel()
    {
        if (Status != ReceiptStatus.Shipping)
        {
            return Result.Failure(ReceiptErrors.NotShipping);
        }

        Status = ReceiptStatus.Cancelled;
        return Result.Success();
    }

    public Result<ReceiptLine> AddLine(
        Guid productId,
        decimal quantityReceived,
        decimal unitPrice)
    {
        var receiptLine = ReceiptLine.Create(productId, quantityReceived, unitPrice);

        _lines.Add(receiptLine);

        return Result.Success(receiptLine);
    }

    public Result UpdateLine(
        Guid purchaseOrderLineId,
        decimal unitPrice,
        decimal qty)
    {
        if (Status != ReceiptStatus.Shipping)
        {
            return Result.Failure(ReceiptErrors.NotShipping);
        }

        ReceiptLine line = _lines.SingleOrDefault(l => l.Id == purchaseOrderLineId);

        if (line == null)
        {
            return Result.Failure(ReceiptErrors.LineNotFound(purchaseOrderLineId));
        }

        line.Update(unitPrice, qty);

        return Result.Success();
    }

    public Result RemoveLine(Guid lineId)
    {
        if (Status != ReceiptStatus.Shipping)
        {
            return Result.Failure(ReceiptErrors.NotShipping);
        }

        ReceiptLine? line = _lines.SingleOrDefault(l => l.Id == lineId);
        if (line == null)
        {
            return Result.Failure(ReceiptErrors.LineNotFound(lineId));
        }

        _lines.Remove(line);

        return Result.Success();
    }

    public Result StoreLine(Guid receivedLineId, decimal Qty)
    {
        ReceiptLine? receiptLine = _lines.SingleOrDefault(l => l.Id == receivedLineId);

        if (receiptLine is null)
        {
            return Result.Failure(ReceiptErrors.LineNotFound(receivedLineId));
        }

        Result result = receiptLine.Store(Qty);

        return result;
    }

    public Result FinalizeLineStorage(Guid receivedLineId)
    {
        ReceiptLine? receiptLine = _lines.SingleOrDefault(l => l.Id == receivedLineId);

        if (receiptLine is null)
        {
            return Result.Failure(ReceiptErrors.LineNotFound(receivedLineId));
        }

        Result result = receiptLine.FinalizeStorage();

        return result;
    }
}
