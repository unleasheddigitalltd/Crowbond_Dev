using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Domain.Customers;
using Crowbond.Modules.OMS.Domain.Payments;
using Crowbond.Modules.OMS.Domain.Products;

namespace Crowbond.Modules.OMS.Domain.Orders;

public sealed class OrderHeader : Entity, IAuditable, ISoftDeletable
{
    private readonly List<OrderLine> _lines = new();
    private readonly List<OrderDelivery> _delivery = new();

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

    public DueDateCalculationBasis DueDateCalculationBasis { get; private set; }

    public int DueDaysForInvoice { get; private set; }

    public PaymentMethod PaymentMethod { get; private set; }

    public DateOnly? PaymentDueDate { get; private set; }

    public string? CustomerComment { get; private set; }

    public string? OriginalSource { get; private set; }

    public string? ExternalOrderRef { get; private set; }

    public string? Tags { get; private set; }

    public int LastImageSequence { get; private set; }

    public OrderStatus Status { get; private set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedOnUtc { get; set; }

    public Guid? LastModifiedBy { get; set; }

    public DateTime? LastModifiedOnUtc { get; set; }

    public bool IsDeleted { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedOnUtc { get; set; }

    public IReadOnlyCollection<OrderLine> Lines => _lines;

    public IReadOnlyCollection<OrderDelivery> Delivery => _delivery;

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
        DueDateCalculationBasis dueDateCalculationBasis,
        int dueDaysForInvoice,
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
            DueDateCalculationBasis = dueDateCalculationBasis,
            DueDaysForInvoice = dueDaysForInvoice,
            PaymentMethod = paymentMethod,
            CustomerComment = customerComment,
            Status = OrderStatus.Pending
        };

        return orderHeader;
    }

    public Result<OrderLine> AddLine(
        Guid productId,
        string productSku,
        string productName,
        string unitOfMeasureName,
        Guid categoryId,
        string categoryName,
        Guid brandId,
        string brandName,
        Guid productGroupId,
        string productGroupName,
        decimal unitPrice,
        decimal qty,
        TaxRateType taxRateType)
    {
        if (Status != OrderStatus.Pending && Status != OrderStatus.StockReviewing)
        {
            return Result.Failure<OrderLine>(OrderErrors.NotStockReviewing);
        }

        var line = OrderLine.Create(
            productId,
            productSku,
            productName,
            unitOfMeasureName,
            categoryId,
            categoryName,
            brandId,
            brandName,
            productGroupId,
            productGroupName,
            unitPrice,
            qty,
            taxRateType);

        _lines.Add(line);
        UpdateTotalAmount();

        return line;
    }

    public Result RemoveLine(Guid orderLineId)
    {
        if (Status != OrderStatus.Pending && Status != OrderStatus.StockReviewing)
        {
            return Result.Failure(OrderErrors.NotStockReviewing);
        }

        OrderLine? line = _lines.SingleOrDefault(ol => ol.Id == orderLineId);

        if (line is null)
        {
            return Result.Failure(OrderErrors.LineNotFound(orderLineId));
        }

        _lines.Remove(line);
        UpdateTotalAmount();

        return Result.Success();
    }

    public Result AdjustLineQty(Guid orderLineId, decimal qty)
    {
        if (Status != OrderStatus.Pending && Status != OrderStatus.StockReviewing)
        {
            return Result.Failure(OrderErrors.NotStockReviewing);
        }

        if (qty < 0)
        {
            return Result.Failure(OrderErrors.InvalidOrderLineQuantity);
        }
        OrderLine? line = _lines.SingleOrDefault(l => l.Id == orderLineId);

        if (line is null)
        {
            return Result.Failure(OrderErrors.LineNotFound(orderLineId));
        }
        line.Update(qty);
        UpdateTotalAmount();

        return Result.Success();
    }

    public void UpdateTotalAmount()
    {
        OrderTax = _lines.Sum(line => line.Tax);
        OrderAmount = _lines.Sum(line => line.LineTotal) + DeliveryCharge;
    }

    public Result Accept()
    {
        if (Status != OrderStatus.Pending && Status != OrderStatus.StockReviewing)
        {
            return Result.Failure(OrderErrors.NotStockReviewing);
        }

        Status = OrderStatus.Accepted;
        Raise(new OrderAcceptedDomainEvent(Id));
        return Result.Success();
    }

    public Result<OrderDelivery> Deliver(Guid routeTripLogId, DateTime utcNow, string? comments)
    {
        if (Status != OrderStatus.Shipped)
        {
            return Result.Failure<OrderDelivery>(OrderErrors.NotShipped);
        }

        var deliveryDate = DateOnly.FromDateTime(utcNow);

        // set payment due date
        PaymentDueDate = GetPaymentDueDate(deliveryDate, DueDateCalculationBasis, DueDaysForInvoice);

        var delivery = OrderDelivery.Create(
            routeTripLogId,
            DeliveryStatus.delivered,
            utcNow,
            comments);

        _delivery.Add(delivery);

        Status = OrderStatus.Delivered;
        return Result.Success(delivery);
    }

    public Result<OrderDelivery> CheckIsDeliver()
    {
        if (Status != OrderStatus.Delivered)
        {
            return Result.Failure<OrderDelivery>(OrderErrors.NotDelivered);
        }

        OrderDelivery? delivery = _delivery.SingleOrDefault(s => s.Status == DeliveryStatus.delivered);

        if (delivery is null)
        {
            return Result.Failure<OrderDelivery>(OrderErrors.NoDeliveryRecordFound);
        }       

        return Result.Success(delivery);
    }

    public OrderDeliveryImage AddDeliveryImage(OrderDelivery orderDelivery, string imageName)
    {
        OrderDeliveryImage orderDeliveryImage = orderDelivery.AddImage(imageName);

        LastImageSequence++;

        return orderDeliveryImage; 
    }

    public Result<OrderDeliveryImage> RemoveDeliveryImage(string imageName)
    {
        if (Status != OrderStatus.Delivered)
        {
            return Result.Failure<OrderDeliveryImage>(OrderErrors.NotDelivered);
        }

        OrderDelivery? delivery = _delivery.SingleOrDefault(s => s.Status == DeliveryStatus.delivered);

        if (delivery is null)
        {
            return Result.Failure<OrderDeliveryImage>(OrderErrors.NoDeliveryRecordFound);
        }

        Result<OrderDeliveryImage> result = delivery.RemoveImage(imageName);

        return result;
    }

    private static DateOnly GetPaymentDueDate(DateOnly deliveryDate, DueDateCalculationBasis dueDateCalculationBasis, int dueDaysForInvoice)
    {
        DateOnly basisDate;

        switch (dueDateCalculationBasis)
        {
            case DueDateCalculationBasis.ShipDate:
                basisDate = deliveryDate;
                break;

            case DueDateCalculationBasis.EndOfInvoiceMonth:
                // Get the year and month from the delivery date
                int year = deliveryDate.Year;
                int month = deliveryDate.Month;

                // Find the last day of the invoice month
                int lastDayOfMonth = DateTime.DaysInMonth(year, month);
                basisDate = new DateOnly(year, month, lastDayOfMonth);
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(dueDateCalculationBasis), dueDateCalculationBasis, "Invalid calculation basis");
        }

        // Add the due days to the calculated basis date
        return basisDate.AddDays(dueDaysForInvoice);
    }

    public Result ValidateRemoval()
    {
        if (Status != OrderStatus.Pending)
        {
            return Result.Failure(OrderErrors.NotPending);
        }

        return Result.Success();
    }

    public Result UpdateOutlet(
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
        string deliveryPostalCode)
    {
        if (Status == OrderStatus.Shipped)
        {
            return Result.Failure(OrderErrors.IsShipped);
        }

        if (Status == OrderStatus.Delivered)
        {
            return Result.Failure(OrderErrors.IsDelivered);
        }

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

        return Result.Success();
    }

    public Result Review()
    {
        if (Status != OrderStatus.Pending)
        {
            return Result.Failure(OrderErrors.NotPending);
        }

        Status = OrderStatus.StockReviewing;
        return Result.Success();
    }
}
