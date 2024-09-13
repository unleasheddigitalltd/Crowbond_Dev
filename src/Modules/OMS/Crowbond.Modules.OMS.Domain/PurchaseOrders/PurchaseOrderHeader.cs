using System;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Domain.Payments;

namespace Crowbond.Modules.OMS.Domain.PurchaseOrders;

public sealed class PurchaseOrderHeader : Entity , IAuditable
{
    private readonly List<PurchaseOrderLine> _lines = new();
    private readonly List<PurchaseOrderStatusHistory> _statusHistory = new();
    private PurchaseOrderHeader()
    {
    }

    public Guid Id { get; private set; }

    public string? PurchaseOrderNo { get; private set; }

    public DateOnly? PurchaseDate { get; private set; }

    public string? PaidBy { get; private set; }

    public DateTime? PaidDate { get; private set; }

    public Guid SupplierId { get; private set; }

    public string SupplierName { get; private set; }

    public string? ContactFullName { get; private set; }

    public string? ContactPhone { get; private set; }

    public string? ContactEmail { get; private set; }

    public decimal PurchaseOrderAmount { get; private set; }

    public string? ShippingLocationName { get; private set; }

    public string? ShippingAddressLine1 { get; private set; }

    public string? ShippingAddressLine2 { get; private set; }

    public string? ShippingTownCity { get; private set; }

    public string? ShippingCounty { get; private set; }

    public string? ShippingCountry { get; private set; }

    public string? ShippingPostalCode { get; private set; }

    public DateOnly RequiredDate { get; private set; }

    public DateOnly? ExpectedShippingDate { get; private set; }

    public string? SupplierReference { get; private set; }

    public decimal PurchaseOrderTax { get; private set; }

    public DeliveryMethod? DeliveryMethod { get; private set; }

    public decimal DeliveryCharge { get; private set; }

    public PaymentMethod PaymentMethod { get; private set; }

    public PaymentStatus PaymentStatus { get; private set; }

    public string? PurchaseOrderNotes { get; private set; }

    public string? SalesOrderRef { get; private set; }

    public string[] Tags { get; private set; }

    public PurchaseOrderStatus Status { get; private set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedOnUtc { get; set; }

    public Guid? LastModifiedBy { get; set; }

    public DateTime? LastModifiedOnUtc { get; set; }

    public IReadOnlyCollection<PurchaseOrderLine> Lines => _lines;

    public IReadOnlyCollection<PurchaseOrderStatusHistory> StatusHistory => _statusHistory;

    public static Result<PurchaseOrderHeader> Create(Guid supplierId, string supplierName, string? contactFullName, string? contactPhone,
        string? contactEmail, DateOnly requiredDate, string? purchaseOrderNotes)
    {
        var orderHeader = new PurchaseOrderHeader
        {
            Id = Guid.NewGuid(),
            SupplierId = supplierId,
            SupplierName = supplierName,
            ContactFullName = contactFullName,
            ContactPhone = contactPhone,
            ContactEmail = contactEmail,
            PurchaseOrderAmount = 0,
            RequiredDate = requiredDate,
            PurchaseOrderTax = 0,
            PaymentMethod = PaymentMethod.Invoice,
            PaymentStatus = PaymentStatus.Unpaid,
            PurchaseOrderNotes = purchaseOrderNotes,
            Status = PurchaseOrderStatus.Draft,
            Tags = []
        };

        return orderHeader;
    }

    public Result UpdateDraft(DateOnly requiredDate, string? purchaseOrderNotes)
    {
        if (Status != PurchaseOrderStatus.Draft)
        {
            return Result.Failure(PurchaseOrderErrors.NotDraft);
        }

        RequiredDate = requiredDate;
        PurchaseOrderNotes = purchaseOrderNotes;

        return Result.Success();
    }

    public Result RemoveLine(Guid lineId)
    {
        if (Status != PurchaseOrderStatus.Draft)
        {
            return Result.Failure(PurchaseOrderErrors.NotDraft);
        }

        PurchaseOrderLine? line = _lines.SingleOrDefault(l => l.Id == lineId);
        if (line == null)
        {
            return Result.Failure(PurchaseOrderErrors.NotFound(lineId));
        }

        _lines.Remove(line);
        UpdateTotalAmount();

        return Result.Success();
    }

    public Result RemoveLines()
    {
        if (Status != PurchaseOrderStatus.Draft)
        {
            return Result.Failure(PurchaseOrderErrors.NotDraft);
        }

        _lines.Clear();
        UpdateTotalAmount();

        return Result.Success();
    }

    public Result<PurchaseOrderLine> AddLine(Guid productId, string productSku, string productName, string unitOfMeasureName, decimal unitPrice, decimal qty,
        TaxRateType taxRateType, bool foc, bool taxable, string? comments)
    {
        if (Status != PurchaseOrderStatus.Draft)
        {
            return Result.Failure<PurchaseOrderLine>(PurchaseOrderErrors.NotDraft);
        }

        Result<PurchaseOrderLine> purchaseOrderLine = PurchaseOrderLine.Create(
            productId,
            productSku,
            productName,
            unitOfMeasureName,
            unitPrice,
            qty,
            taxRateType,
            foc,
            taxable,
            comments);

        if (purchaseOrderLine.IsFailure)
        {
            return Result.Failure<PurchaseOrderLine>(purchaseOrderLine.Error);
        }

        _lines.Add(purchaseOrderLine.Value);
        UpdateTotalAmount();
        return purchaseOrderLine.Value;
    }

    public Result UpdateLine(Guid purchaseOrderLineId, decimal qty, string? comments)
    {
        if (Status != PurchaseOrderStatus.Draft)
        {
            return Result.Failure(PurchaseOrderErrors.NotDraft);
        }

        PurchaseOrderLine? line = _lines.SingleOrDefault(pl => pl.Id == purchaseOrderLineId);

        if (line is null)
        {
            return Result.Failure(PurchaseOrderErrors.NotFound(purchaseOrderLineId));
        }

        line.Update(qty, comments);
        UpdateTotalAmount();

        return Result.Success();
    }

    public void UpdateDetails(
    string? contactFullName,
    string? contactPhone,
    string? contactEmail,
    string? shippingLocationName,
    string? shippingAddressLine1,
    string? shippingAddressLine2,
    string? shippingTownCity,
    string? shippingCounty,
    string? shippingCountry,
    string? shippingPostalCode,
    DateOnly requiredDate,
    DateOnly? expectedShippingDate,
    string? supplierReference,
    DeliveryMethod? deliveryMethod,
    decimal deliveryCharge,
    PaymentMethod paymentMethod,
    string? purchaseOrderNotes,
    string? salesOrderRef)
    {
        ContactFullName = contactFullName;
        ContactPhone = contactPhone;
        ContactEmail = contactEmail;
        ShippingLocationName = shippingLocationName;
        ShippingAddressLine1 = shippingAddressLine1;
        ShippingAddressLine2 = shippingAddressLine2;
        ShippingTownCity = shippingTownCity;
        ShippingCounty = shippingCounty;
        ShippingCountry = shippingCountry;
        ShippingPostalCode = shippingPostalCode;
        RequiredDate = requiredDate;
        ExpectedShippingDate = expectedShippingDate;
        SupplierReference = supplierReference;
        DeliveryMethod = deliveryMethod;
        DeliveryCharge = deliveryCharge;
        PaymentMethod = paymentMethod;
        PurchaseOrderNotes = purchaseOrderNotes;
        SalesOrderRef = salesOrderRef;
        UpdateTotalAmount();
    }

    public Result<PurchaseOrderStatusHistory> Draft(DateTime utcNow)
    {
        if (Status != PurchaseOrderStatus.Pending)
        {
            return Result.Failure<PurchaseOrderStatusHistory>(PurchaseOrderErrors.NotPending);
        }

        Status = PurchaseOrderStatus.Draft;

        var newHistory = new PurchaseOrderStatusHistory(Status, utcNow);
        _statusHistory.Add(newHistory);

        return newHistory;
    }

    public Result<PurchaseOrderStatusHistory> Pend(DateTime utcNow)
    {
        if (Status != PurchaseOrderStatus.Draft)
        {
            return Result.Failure<PurchaseOrderStatusHistory>(PurchaseOrderErrors.NotDraft);
        }

        Status = PurchaseOrderStatus.Pending;

        var newHistory = new PurchaseOrderStatusHistory(Status, utcNow);
        _statusHistory.Add(newHistory);

        return newHistory;
    }

    public Result<PurchaseOrderStatusHistory> Approve(string purchaseOrderNo, DateTime utcNow)
    {
        if (Status != PurchaseOrderStatus.Pending)
        {
            return Result.Failure<PurchaseOrderStatusHistory>(PurchaseOrderErrors.NotPending);
        }

        Status = PurchaseOrderStatus.Approved;
        PurchaseOrderNo = purchaseOrderNo;
        PurchaseDate = DateOnly.FromDateTime(utcNow);

        var newHistory = new PurchaseOrderStatusHistory(Status, utcNow);
        _statusHistory.Add(newHistory);

        Raise(new PurchaseOrderApprovedDomainEvent(Id));

        return newHistory;
    }

    public Result<PurchaseOrderStatusHistory> Cancel(DateTime utcNow)
    {
        if (Status != PurchaseOrderStatus.Approved)
        {
            return Result.Failure<PurchaseOrderStatusHistory>(PurchaseOrderErrors.NotApproved);
        }

        Status = PurchaseOrderStatus.Cancelled;

        var newHistory = new PurchaseOrderStatusHistory(Status, utcNow);
        _statusHistory.Add(newHistory);

        Raise(new PurchaseOrderCancelledDomainEvent(Id));

        return newHistory;
    }

    public void UpdateTags(string[] tags)
    {
        Tags = tags;
    }

    public Result UpdatePayment(string paidBy, DateTime paidDate)
    {
        if (PaymentStatus == PaymentStatus.Paid)
        {
            return Result.Failure(PurchaseOrderErrors.AlreadyPaid);
        }

        PaymentStatus = PaymentStatus.Paid;
        PaidBy = paidBy;
        PaidDate = paidDate;

        return Result.Success();
    }

    public void UpdateTotalAmount()
    {
        PurchaseOrderTax = _lines.Sum(line => line.Tax);
        PurchaseOrderAmount = _lines.Sum(line => line.LineTotal) + DeliveryCharge;
    }
}
