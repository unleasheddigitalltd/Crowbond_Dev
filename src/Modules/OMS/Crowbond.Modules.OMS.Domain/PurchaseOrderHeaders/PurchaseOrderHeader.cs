using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Domain.Payments;
using Crowbond.Modules.OMS.Domain.PurchaseOrderLines;

namespace Crowbond.Modules.OMS.Domain.PurchaseOrderHeaders;

public sealed class PurchaseOrderHeader : Entity
{
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

    public Guid CreateBy { get; private set; }

    public DateTime CreateDate { get; private set; }

    public Guid? LastModifiedBy { get; private set; }

    public DateTime? LastModifiedDate { get; private set; }

    public List<PurchaseOrderLine> PurchaseOrderLines { get; private set; }

    public static Result<PurchaseOrderHeader> Create(
        Guid supplierId,
        string supplierName,
        string? contactFullName,
        string? contactPhone,
        string? contactEmail,
        DateOnly requiredDate,
        string? purchaseOrderNotes,
        Guid createBy,
        DateTime createDate)
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
            Tags = [],
            PurchaseOrderLines = new List<PurchaseOrderLine>()
        };

        return orderHeader;
    }

    public void Update(
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
            return Result.Failure(PurchaseOrderHeaderErrors.NotDraft);
        }

        Status = PurchaseOrderStatus.Pending;

        return Result.Success();
    }

    public Result Approve(string purchaseOrderNo, DateOnly purchaseDate)
    {
        if (Status != PurchaseOrderStatus.Pending)
        {
            return Result.Failure(PurchaseOrderHeaderErrors.NotPending);
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
            return Result.Failure(PurchaseOrderHeaderErrors.AlreadyPaid);
        }

        PaymentStatus = PaymentStatus.Paid;
        PaidBy = paidBy;
        PaidDate = paidDate;

        return Result.Success();
    }

    public void AddPurchaseOrderLine(PurchaseOrderLine purchaseOrderLine)
    {
        PurchaseOrderLines.Add(purchaseOrderLine);
        UpdateTotalAmount();
    }

    public void UpdateTotalAmount()
    {
        PurchaseOrderTax = PurchaseOrderLines.Sum(line => line.Tax);
        PurchaseOrderAmount = PurchaseOrderLines.Sum(line => line.LineTotal) + DeliveryCharge;
    }
}
