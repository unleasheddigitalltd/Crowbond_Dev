using Crowbond.Common.Domain;

namespace Crowbond.Modules.OMS.Domain.Orders;

public static class OrderErrors
{
    public static Error NotFound(Guid orderId) =>
        Error.NotFound("Orders.NotFound", $"The order with the identifier {orderId} was not found");

    public readonly static Error SequenceNotFound =
        Error.NotFound("Sequence.NotFound", $"The sequence for the order type was not found");

    public static readonly Error InvalidPaymentMethod = 
        Error.Problem("Orders.InvalidPaymentMethod", "The payment method is invalid");

    public static readonly Error InvalidDeliveryMethod = 
        Error.Problem("Orders.InvalidDeliveryMethod", "The delivery method is invalid");

    public static readonly Error InvalidPaymentTerm = 
        Error.Problem("Orders.InvalidPaymentTerm", "The payment term is invalid");

    public static readonly Error InvalidDeliveryFeeSetting = 
        Error.Problem("Orders.InvalidDeliveryFeeSetting", "The delivery fee setting is invalid");

    public static readonly Error InvalidShippingDateError = 
        Error.Problem("Orders.InvalidShippingDateError", "Shipping date must be later than today's date");
}
