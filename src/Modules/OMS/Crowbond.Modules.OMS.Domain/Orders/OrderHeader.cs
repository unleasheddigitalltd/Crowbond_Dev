using Crowbond.Common.Domain;

namespace Crowbond.Modules.OMS.Domain.Orders;

public sealed class OrderHeader : Entity
{
    public OrderHeader()
    {        
    }

    public Guid Id { get; private set; }

    public string InvoiceNo { get; private set; }

    public string? InvoicedBy { get; private set; }

    public DateTime? InvoicedDate { get; private set; }

    public Guid CustomerId { get; private set; }

    public string CustomerName { get; private set; }

    public string CustomerMobile { get; private set; }

    public string CustomerEmail { get; private set; }

    public decimal OrderAmount { get; private set; }

    public string DriverCode { get; private set; }

    public string ShippingAddressCompany { get; private set; }

    public string ShippingAddressLine1 { get; private set; }

    public string ShippingAddressLine2 { get; private set; }

    public string ShippingAddressTownCity { get; private set; }

    public string? ShippingAddressCounty { get; private set; }

    public string ShippingAddressCountry { get; private set; }

    public string ShippingAddressPostalCode { get; private set; }

    public DateOnly ShippingDate { get; private set; }

    public string SalesOrderNumber { get; private set; }

    public string? PurchaseOrderNumber { get; private set; }

    public decimal OrderTax { get; private set; }

    public DeliveryMethod? DeliveryMethod { get; private set; }

    public decimal DeliveryCharge { get; private set; }

    public PaymentMethod PaymentMethod { get; private set; }

    public PaymentStatus PaymentStatus { get; private set; }

    public string? CustomerComment { get; private set; }

    public string OriginalSource { get; private set; }

    public string ExternalOrderRef { get; private set; }

    public string Tags { get; private set; }

    public static Result<OrderHeader> Create(
    string invoiceNo,
    string? invoicedBy,
    DateTime? invoicedDate,
    Guid customerId,
    string customerName,
    string customerMobile,
    string customerEmail,
    decimal orderAmount,
    string driverCode,
    string shippingAddressCompany,
    string shippingAddressLine1,
    string shippingAddressLine2,
    string shippingAddressTownCity,
    string? shippingAddressCounty,
    string shippingAddressCountry,
    string shippingAddressPostalCode,
    DateOnly shippingDate,
    string salesOrderNumber,
    string? purchaseOrderNumber,
    decimal orderTax,
    DeliveryMethod? deliveryMethod,
    decimal deliveryCharge,
    PaymentMethod paymentMethod,
    PaymentStatus paymentStatus,
    string? customerComment,
    string originalSource,
    string externalOrderRef,
    string tags)
    {
        var orderHeader = new OrderHeader
        {
            Id = Guid.NewGuid(),
            InvoiceNo = invoiceNo,
            InvoicedBy = invoicedBy,
            InvoicedDate = invoicedDate,
            CustomerId = customerId,
            CustomerName = customerName,
            CustomerMobile = customerMobile,
            CustomerEmail = customerEmail,
            OrderAmount = orderAmount,
            DriverCode = driverCode,
            ShippingAddressCompany = shippingAddressCompany,
            ShippingAddressLine1 = shippingAddressLine1,
            ShippingAddressLine2 = shippingAddressLine2,
            ShippingAddressTownCity = shippingAddressTownCity,
            ShippingAddressCounty = shippingAddressCounty,
            ShippingAddressCountry = shippingAddressCountry,
            ShippingAddressPostalCode = shippingAddressPostalCode,
            ShippingDate = shippingDate,
            SalesOrderNumber = salesOrderNumber,
            PurchaseOrderNumber = purchaseOrderNumber,
            OrderTax = orderTax,
            DeliveryMethod = deliveryMethod,
            DeliveryCharge = deliveryCharge,
            PaymentMethod = paymentMethod,
            PaymentStatus = paymentStatus,
            CustomerComment = customerComment,
            OriginalSource = originalSource,
            ExternalOrderRef = externalOrderRef,
            Tags = tags
        };

        return orderHeader;
    }

}
