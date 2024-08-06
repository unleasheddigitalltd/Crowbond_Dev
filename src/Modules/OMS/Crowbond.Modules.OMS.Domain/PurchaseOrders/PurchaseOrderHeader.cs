using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Domain.Payments;

namespace Crowbond.Modules.OMS.Domain.PurchaseOrders;

public sealed class PurchaseOrderHeader : Entity
{
    private PurchaseOrderHeader()
    {        
    }

    public Guid Id { get; private set; }

    public string PurchaseOrderNo { get; private set; }

    public string? PaidBy { get; private set; }

    public DateTime? PaidDate { get; private set; }

    public Guid SupplierId { get; private set; }

    public string SupplierName { get; private set; }

    public string SupplierPhone { get; private set; }

    public string SupplierEmail { get; private set; }

    public string SupplierContact { get; private set; }

    public decimal PurchaseOrderAmount { get; private set; }

    public string LocationName { get; private set; }

    public string ShippingAddressLine1 { get; private set; }

    public string? ShippingAddressLine2 { get; private set; }

    public string ShippingAddressTownCity { get; private set; }

    public string? ShippingAddressCounty { get; private set; }

    public string? ShippingAddressCountry { get; private set; }

    public string ShippingAddressPostalCode { get; private set; }

    public DateOnly RequiredDate { get; private set; }

    public DateOnly PurchaseDate { get; private set; }

    public DateOnly ExpectedShippingDate { get; private set; }

    public string? SupplierReference { get; private set; }

    public decimal PurchaseOrderTax { get; private set; }

    public DeliveryMethod? DeliveryMethod { get; private set; }

    public decimal DeliveryCharge { get; private set; }

    public PaymentMethod PaymentMethod { get; private set; }

    public PaymentStatus PaymentStatus { get; private set; }

    public string? PurchaseOrderNotes { get; private set; }

    public string SalesOrderRef { get; private set; }

    public string Tags { get; private set; }

    public static Result<PurchaseOrderHeader> Create(
    string purchaseOrderNo,
    string? paidBy,
    DateTime? paidDate,
    Guid supplierId,
    string supplierName,
    string supplierPhone,
    string supplierEmail,
    string supplierContact,
    decimal purchaseOrderAmount,
    string shippingAddressLine1,
    string? shippingAddressLine2,
    string shippingAddressTownCity,
    string shippingAddressCounty,
    string? shippingAddressCountry,
    string shippingAddressPostalCode,
    DateOnly requiredDate,
    DateOnly purchaseDate,
    DateOnly expectedShippingDate,
    string? supplierReference,
    decimal purchaseOrderTax,
    DeliveryMethod? deliveryMethod,
    decimal deliveryCharge,
    PaymentMethod paymentMethod,
    PaymentStatus paymentStatus,
    string? purchaseOrderNotes,
    string salesOrderRef,
    string tags)
    {
        var orderHeader = new PurchaseOrderHeader
        {
            Id = Guid.NewGuid(),
            PurchaseOrderNo = purchaseOrderNo,
            PaidBy = paidBy,
            PaidDate = paidDate,
            SupplierId = supplierId,
            SupplierName = supplierName,
            SupplierPhone = supplierPhone,
            SupplierEmail = supplierEmail,
            SupplierContact = supplierContact,
            PurchaseOrderAmount = purchaseOrderAmount,
            ShippingAddressLine1 = shippingAddressLine1,
            ShippingAddressLine2 = shippingAddressLine2,
            ShippingAddressTownCity = shippingAddressTownCity,
            ShippingAddressCounty = shippingAddressCounty,
            ShippingAddressCountry = shippingAddressCountry,
            ShippingAddressPostalCode = shippingAddressPostalCode,
            RequiredDate = requiredDate,
            PurchaseDate = purchaseDate,
            ExpectedShippingDate = expectedShippingDate,
            SupplierReference = supplierReference,
            PurchaseOrderTax = purchaseOrderTax,
            DeliveryMethod = deliveryMethod,
            DeliveryCharge = deliveryCharge,
            PaymentMethod = paymentMethod,
            PaymentStatus = paymentStatus,
            PurchaseOrderNotes = purchaseOrderNotes,
            SalesOrderRef = salesOrderRef,
            Tags = tags
        };

        return orderHeader;
    }

    public void Update(
     string supplierName,
     string shippingAddressLine1,
     string? shippingAddressLine2,
     string shippingAddressTownCity,
     string shippingAddressCounty,
     string? shippingAddressCountry,
     string shippingAddressPostalCode,
     string? purchaseOrderNotes,
     string supplierEmail,
     string supplierPhone,
     string supplierContact)
    {

        SupplierName = supplierName;
        ShippingAddressLine1 = shippingAddressLine1;
        ShippingAddressLine2 = shippingAddressLine2;
        ShippingAddressTownCity = shippingAddressTownCity;
        ShippingAddressCounty = shippingAddressCounty;
        ShippingAddressCountry = shippingAddressCountry;
        ShippingAddressPostalCode = shippingAddressPostalCode;
        PurchaseOrderNotes = purchaseOrderNotes;
        SupplierEmail = supplierEmail;
        SupplierPhone = supplierPhone;
        SupplierContact = supplierContact;
    }
}
