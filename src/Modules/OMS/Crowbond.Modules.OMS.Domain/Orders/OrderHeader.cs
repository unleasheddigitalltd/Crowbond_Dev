using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Domain.Payments;

namespace Crowbond.Modules.OMS.Domain.Orders;

public sealed class OrderHeader : Entity
{
    public OrderHeader()
    {        
    }

    public Guid Id { get; private set; }

    public string OrderNumber { get; private set; }

    public string? PurchaseOrderNumber { get; private set; }

    public Guid CustomerId { get; private set; }

    public string CustomerBusinessName { get; private set; }

    public string DeliveryLocationName { get; private set; }

    public string DeliveryFullName { get; private set; }

    public string? DeliveryEmail { get; private set; }

    public string DeliveryPhone { get; private set; }

    public string? DeliveryMobile { get; private set; }

    public string? DeliveryNotes { get; private set; }

    public string DeliveryAddressLine1 { get; private set; }

    public string DeliveryAddressLine2 { get; private set; }

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

    public PaymentMethod PaymentMethod { get; private set; }

    public string? CustomerComment { get; private set; }

    public string? OriginalSource { get; private set; }

    public string? ExternalOrderRef { get; private set; }

    public string? Tags { get; private set; }

    public OrderStatus Status { get; private set; }

    public static Result<OrderHeader> Create(
        string orderNumber,
        string? purchaseOrderNumber,
        Guid customerId,
        string customerBusinessName,
        string deliveryLocationName,
        string deliveryFullName,
        string? deliveryEmail,
        string deliveryPhone,
        string? deliveryMobile,
        string? deliveryNotes,
        string deliveryAddressLine1,
        string deliveryAddressLine2,
        string deliveryTownCity,
        string deliveryCounty,
        string? deliveryCountry,
        string deliveryPostalCode,
        DateOnly shippingDate,
        DeliveryMethod deliveryMethod,
        decimal deliveryCharge,
        decimal orderAmount,
        decimal orderTax,
        PaymentStatus paymentStatus,
        PaymentMethod paymentMethod,
        string? customerComment,
        string? originalSource,
        string? externalOrderRef,
        string? tags)
    {
        var orderHeader = new OrderHeader
        {
            Id = Guid.NewGuid(),
            OrderNumber = orderNumber,
            PurchaseOrderNumber = purchaseOrderNumber,
            CustomerId = customerId,
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
            OrderAmount = orderAmount,
            OrderTax = orderTax,
            PaymentStatus = paymentStatus,
            PaymentMethod = paymentMethod,
            CustomerComment = customerComment,
            OriginalSource = originalSource,
            ExternalOrderRef = externalOrderRef,
            Tags = tags,
            Status = OrderStatus.Pending
        };

        return orderHeader;
    }

    public void Update(
        string? purchaseOrderNumber,
        Guid customerId,
        string customerBusinessName,
        string deliveryLocationName,
        string deliveryFullName,
        string? deliveryEmail,
        string deliveryPhone,
        string? deliveryMobile,
        string? deliveryNotes,
        string deliveryAddressLine1,
        string deliveryAddressLine2,
        string deliveryTownCity,
        string deliveryCounty,
        string? deliveryCountry,
        string deliveryPostalCode,
        DateOnly shippingDate,
        Guid? routeTripId,
        string? routeName,
        DeliveryMethod deliveryMethod,
        decimal deliveryCharge,
        decimal orderAmount,
        decimal orderTax,
        PaymentStatus paymentStatus,
        PaymentMethod paymentMethod,
        string? customerComment,
        string? originalSource,
        string? externalOrderRef,
        string? tags)
    {
        PurchaseOrderNumber = purchaseOrderNumber;
        CustomerId = customerId;
        CustomerBusinessName = customerBusinessName;
        DeliveryLocationName = deliveryLocationName;
        DeliveryFullName = deliveryFullName;
        DeliveryEmail = deliveryEmail;
        DeliveryPhone = deliveryPhone;
        DeliveryMobile = deliveryMobile;
        DeliveryNotes = deliveryNotes;
        DeliveryAddressLine1 = deliveryAddressLine1;
        DeliveryAddressLine2 = deliveryAddressLine2;
        DeliveryTownCity = deliveryTownCity;
        DeliveryCounty = deliveryCounty;
        DeliveryCountry = deliveryCountry;
        DeliveryPostalCode = deliveryPostalCode;
        ShippingDate = shippingDate;
        RouteTripId = routeTripId;
        RouteName = routeName;
        DeliveryMethod = deliveryMethod;
        DeliveryCharge = deliveryCharge;
        OrderAmount = orderAmount;
        OrderTax = orderTax;
        PaymentStatus = paymentStatus;
        PaymentMethod = paymentMethod;
        CustomerComment = customerComment;
        OriginalSource = originalSource;
        ExternalOrderRef = externalOrderRef;
        Tags = tags;
    }
}
