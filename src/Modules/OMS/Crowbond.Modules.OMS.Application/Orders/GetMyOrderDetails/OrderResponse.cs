﻿using Crowbond.Modules.OMS.Domain.Customers;
using Crowbond.Modules.OMS.Domain.Orders;
using Crowbond.Modules.OMS.Domain.Payments;

namespace Crowbond.Modules.OMS.Application.Orders.GetMyOrderDetails;

public sealed record OrderResponse(
    Guid Id,
    string OrderNo,
    string? PurchaseOrderNo,
    Guid CustomerId,
    string CustomerAccountNumber,
    string CustomerBusinessName,
    string DeliveryLocationName,
    string DeliveryFullName,
    string? DeliveryEmail,
    string DeliveryPhone,
    string? DeliveryMobile,
    string? DeliveryNotes,
    string DeliveryAddressLine1,
    string? DeliveryAddressLine2,
    string DeliveryTownCity,
    string DeliveryCounty,
    string? DeliveryCountry,
    string DeliveryPostalCode,
    DateOnly ShippingDate,
    Guid? RouteTripId,
    string? RouteName,
    DeliveryMethod DeliveryMethod,
    decimal DeliveryCharge,
    decimal OrderAmount,
    decimal OrderTax,
    PaymentStatus PaymentStatus,
    DueDateCalculationBasis DueDateCalculationBasis,
    int DueDaysForInvoice,
    PaymentMethod PaymentMethod,
    DateOnly? PaymentDueDate,
    string? CustomerComment,
    string? OriginalSource,
    string? ExternalOrderRef,
    string? Tags,
    OrderStatus Status)
{
    public List<OrderLineResponse> OrderLines { get; } = [];
}
