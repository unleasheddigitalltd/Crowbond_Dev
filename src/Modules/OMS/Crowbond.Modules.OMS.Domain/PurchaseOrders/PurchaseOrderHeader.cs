using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Domain.Payments;

namespace Crowbond.Modules.OMS.Domain.PurchaseOrders;

public sealed class PurchaseOrderHeader : Entity
{
    private PurchaseOrderHeader()
    {
        PurchaseOrderLines = new List<PurchaseOrderLine>();
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

    public Guid CreateBy { get; private set; }

    public DateTime CreateDate { get; private set; }

    public Guid? LastModifiedBy { get; private set; }

    public DateTime? LastModifiedDate { get; private set; }

    public List<PurchaseOrderLine> PurchaseOrderLines { get; private set; }

    public static Result<PurchaseOrderHeader> Create(Guid supplierId, string supplierName, string? contactFullName, string? contactPhone,
        string? contactEmail, DateOnly requiredDate, string? purchaseOrderNotes, Guid createBy, DateTime createDate)
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
            CreateBy = createBy,
            CreateDate = createDate,
            Tags = []
        };

        return orderHeader;
    }

    public Result UpdateDraft(DateOnly requiredDate, string? purchaseOrderNotes, Guid lastModifiedBy, DateTime lastModifiedDate)
    {
        if (Status != PurchaseOrderStatus.Draft)
        {
            return Result.Failure(PurchaseOrderErrors.NotDraft);
        }

        RequiredDate = requiredDate;
        PurchaseOrderNotes = purchaseOrderNotes;
        LastModifiedBy = lastModifiedBy;
        LastModifiedDate = lastModifiedDate;

        return Result.Success();
    }

    public Result RemoveLine(Guid lineId)
    {
        if (Status != PurchaseOrderStatus.Draft)
        {
            return Result.Failure(PurchaseOrderErrors.NotDraft);
        }

        PurchaseOrderLine? line = PurchaseOrderLines.SingleOrDefault(l => l.Id == lineId);
        if (line == null)
        {
            return Result.Failure(PurchaseOrderErrors.NotFound(lineId));
        }

        PurchaseOrderLines.Remove(line);
        UpdateTotalAmount();

        return Result.Success();
    }

    public Result RemoveLines()
    {
        if (Status != PurchaseOrderStatus.Draft)
        {
            return Result.Failure(PurchaseOrderErrors.NotDraft);
        }

        PurchaseOrderLines.Clear();
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

        PurchaseOrderLines.Add(purchaseOrderLine.Value);
        UpdateTotalAmount();
        return purchaseOrderLine.Value;
    }

    public Result UpdateLine(Guid purchaseOrderLineId, decimal qty, string? comments)
    {
        if (Status != PurchaseOrderStatus.Draft)
        {
            return Result.Failure(PurchaseOrderErrors.NotDraft);
        }

        PurchaseOrderLine? line = PurchaseOrderLines.SingleOrDefault(pl => pl.Id == purchaseOrderLineId);

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
    string? salesOrderRef,
    Guid lastModifiedBy,
    DateTime lastModifiedDate)
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
        LastModifiedBy = lastModifiedBy;
        LastModifiedDate = lastModifiedDate;
        UpdateTotalAmount();
    }

    public Result Pend()
    {
        if (Status != PurchaseOrderStatus.Draft)
        {
            return Result.Failure(PurchaseOrderErrors.NotDraft);
        }

        Status = PurchaseOrderStatus.Pending;

        return Result.Success();
    }

    public Result Approve(string purchaseOrderNo, DateOnly purchaseDate)
    {
        if (Status != PurchaseOrderStatus.Pending)
        {
            return Result.Failure(PurchaseOrderErrors.NotPending);
        }

        Status = PurchaseOrderStatus.Approved;
        PurchaseOrderNo = purchaseOrderNo;
        PurchaseDate = purchaseDate;

        return Result.Success();
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
        PurchaseOrderTax = PurchaseOrderLines.Sum(line => line.Tax);
        PurchaseOrderAmount = PurchaseOrderLines.Sum(line => line.LineTotal) + DeliveryCharge;
    }

}
