using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Domain.Customers;
using Crowbond.Modules.OMS.Domain.Payments;
using Crowbond.Modules.OMS.Domain.Products;

namespace Crowbond.Modules.OMS.Domain.Orders;

public sealed class OrderHeader : Entity, IAuditable
{
    private readonly List<OrderLine> _lines = new();

    private OrderHeader()
    {        
    }

    public Guid Id { get; private set; }

    public string OrderNo { get; private set; }

    public string? PurchaseOrderNo { get; private set; }

    public Guid CustomerId { get; private set; }

    public string CustomerAccountNumber { get; private set; }

    public string CustomerBusinessName { get; private set; }

    public string DeliveryLocationName { get; private set; }

    public string DeliveryFullName { get; private set; }

    public string? DeliveryEmail { get; private set; }

    public string DeliveryPhone { get; private set; }

    public string? DeliveryMobile { get; private set; }

    public string? DeliveryNotes { get; private set; }

    public string DeliveryAddressLine1 { get; private set; }

    public string? DeliveryAddressLine2 { get; private set; }

    public string DeliveryTownCity { get; private set; }

    public string DeliveryCounty { get; private set; }

    public string? DeliveryCountry { get; private set; }

    public string DeliveryPostalCode { get; private set; }

    public DateOnly ShippingDate { get; private set; }

    public Guid? RouteTripId { get; private set; }

    public string? RouteName { get; private set; }

    public DeliveryMethod DeliveryMethod { get; private set; }

    public decimal DeliveryCharge { get; private set; }

    public decimal OrderAmount { get; private set; }

    public decimal OrderTax { get; private set; }

    public PaymentStatus PaymentStatus { get; private set; }

    public PaymentTerm PaymentTerm { get; private set; }

    public PaymentMethod PaymentMethod { get; private set; }

    public DateOnly? PaymentDueDate { get; private set; }

    public string? CustomerComment { get; private set; }

    public string? OriginalSource { get; private set; }

    public string? ExternalOrderRef { get; private set; }

    public string? Tags { get; private set; }

    public OrderStatus Status { get; private set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedOnUtc { get; set; }

    public Guid? LastModifiedBy { get; set; }

    public DateTime? LastModifiedOnUtc { get; set; }

    public IReadOnlyCollection<OrderLine> Lines => _lines;

    public static Result<OrderHeader> Create(
        string orderNo,
        string? purchaseOrderNo,
        Guid customerId,
        string customerAccountNumber,
        string customerBusinessName,
        string deliveryLocationName,
        string deliveryFullName,
        string? deliveryEmail,
        string deliveryPhone,
        string? deliveryMobile,
        string? deliveryNotes,
        string deliveryAddressLine1,
        string? deliveryAddressLine2,
        string deliveryTownCity,
        string deliveryCounty,
        string? deliveryCountry,
        string deliveryPostalCode,
        DateOnly shippingDate,
        DeliveryMethod deliveryMethod,
        decimal deliveryCharge,
        PaymentTerm paymentTerm,
        PaymentMethod paymentMethod,
        string? customerComment,
        DateTime utcNow)
    {
        var today = DateOnly.FromDateTime(utcNow);

        if (shippingDate <= today)
        {
            return Result.Failure<OrderHeader>(OrderErrors.InvalidShippingDateError);
        }

        var orderHeader = new OrderHeader
        {
            Id = Guid.NewGuid(),
            OrderNo = orderNo,
            PurchaseOrderNo = purchaseOrderNo,
            CustomerId = customerId,
            CustomerAccountNumber = customerAccountNumber,
            CustomerBusinessName = customerBusinessName,
            DeliveryLocationName = deliveryLocationName,
            DeliveryFullName = deliveryFullName,
            DeliveryEmail = deliveryEmail,
            DeliveryPhone = deliveryPhone,
            DeliveryMobile = deliveryMobile,
            DeliveryNotes = deliveryNotes,
            DeliveryAddressLine1 = deliveryAddressLine1,
            DeliveryAddressLine2 = deliveryAddressLine2,
            DeliveryTownCity = deliveryTownCity,
            DeliveryCounty = deliveryCounty,
            DeliveryCountry = deliveryCountry,
            DeliveryPostalCode = deliveryPostalCode,
            ShippingDate = shippingDate,
            DeliveryMethod = deliveryMethod,
            DeliveryCharge = deliveryCharge,
            OrderAmount = 0,
            OrderTax = 0,
            PaymentStatus = PaymentStatus.Unpaid,
            PaymentTerm = paymentTerm,
            PaymentMethod = paymentMethod,
            CustomerComment = customerComment,
            Status = OrderStatus.Pending
        };

        return orderHeader;
    }

    public OrderLine AddLine(
        Guid productId,
        string productSku,
        string productName,
        string unitOfMeasureName,
        decimal unitPrice,
        decimal qty,
        TaxRateType taxRateType)
    {
        var line = OrderLine.Create(
            productId,
            productSku,
            productName,
            unitOfMeasureName,
            unitPrice,
            qty,
            taxRateType);

        _lines.Add(line);
        UpdateTotalAmount();

        return line;
    }

    public void UpdateTotalAmount()
    {
        OrderTax = _lines.Sum(line => line.Tax);
        OrderAmount = _lines.Sum(line => line.LineTotal) + DeliveryCharge;
    }

    public void Deliver(DateTime utcNow)
    {
        var deliveryDate = DateOnly.FromDateTime(utcNow);
        // set payment due date
        PaymentDueDate = PaymentTerm switch
        {
            PaymentTerm.ShortTerm => deliveryDate.AddDays(7),
            PaymentTerm.StandardTerm => GetStandardPaymentDate(deliveryDate),
            PaymentTerm.LongTerm => deliveryDate.AddDays(28),
            _ => throw new NotImplementedException()
        };

        Status = OrderStatus.Delivered;
    }

    private static DateOnly GetStandardPaymentDate(DateOnly deliveryDate)
    {
        // Get the year and month from the delivery date
        int year = deliveryDate.Year;
        int month = deliveryDate.Month;

        // Find the last day of the month
        int lastDayOfMonth = DateTime.DaysInMonth(year, month);
        var monthEndDate = new DateOnly(year, month, lastDayOfMonth);

        // Add 14 days to the last day of the month
        DateOnly result = monthEndDate.AddDays(14);

        return result;
    }
}
