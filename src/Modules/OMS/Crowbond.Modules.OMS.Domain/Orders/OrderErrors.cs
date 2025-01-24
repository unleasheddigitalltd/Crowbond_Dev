using Crowbond.Common.Domain;

namespace Crowbond.Modules.OMS.Domain.Orders;

public static class OrderErrors
{
    public static Error NotFound(Guid orderId) =>
        Error.NotFound("Orders.NotFound", $"The order with the identifier {orderId} was not found");

    public static Error NoRouteTripHasAssigned(Guid orderId) =>
        Error.NotFound("Orders.NoRouteTripHasAssigned", $"The order with the identifier {orderId} has no route trip assigned");

    public static Error LineNotFound(Guid orderLineId) =>
        Error.NotFound("Orders.LineNotFound", $"The order line with the identifier {orderLineId} was not found");

    public static Error LineRejectNotFound(Guid orderLineRejectId) =>
        Error.NotFound("Orders.LineRejectNotFound", $"The order line reject with the identifier {orderLineRejectId} was not found");

    public static Error LineRejectResultNotFound(Guid orderLineRejectResultId) =>
        Error.NotFound("Orders.LineRejectResultNotFound", $"The order line reject result with the identifier {orderLineRejectResultId} was not found");

    public static Error LineForProductExists(Guid productId) =>
        Error.NotFound("Orders.LineForProductExists", $"An order line for the product with the identifier {productId} already exists");

    public static Error DeliveryImageNotFound(string imageName) =>
        Error.NotFound("Orders.DeliveryImageNotFound", $"The order delivery image with the name {imageName} was not found");

    public static Error LineHasShortage(Guid orderLineId) =>
        Error.NotFound("Orders.LineHasShortage", $"The order line with the identifier {orderLineId} has shortage");

    public static Error NotAssignedToRouteTrip(Guid orderId) =>
        Error.NotFound("Orders.NotAssignedToRouteTrip", $"The order with the identifier {orderId} is not assigned to any route trip");

    public static Error ProductIsBlacklisted(Guid productId) =>
        Error.NotFound("Orders.ProductIsBlacklisted", $"The product with the identifier {productId} is blacklisted");

    public static Error LogDateMismatch(Guid routeTripId) =>
        Error.NotFound("Orders.LogDateMismatch", $"Log entry date for route trip with the identifier {routeTripId} does not match today's date");

    public readonly static Error SequenceNotFound =
        Error.NotFound("Sequence.NotFound", $"The sequence for the order type was not found");

    public static readonly Error InvalidPaymentMethod =
        Error.Problem("Orders.InvalidPaymentMethod", "The payment method is invalid");

    public static readonly Error InvalidDeliveryMethod =
        Error.Problem("Orders.InvalidDeliveryMethod", "The delivery method is invalid");

    public static readonly Error InvalidDueDateCalculationBasis =
        Error.Problem("Orders.InvalidDueDateCalculationBasis", "The due date calculation basis is invalid");

    public static readonly Error InvalidDeliveryFeeSetting =
        Error.Problem("Orders.InvalidDeliveryFeeSetting", "The delivery fee setting is invalid");

    public static readonly Error InvalidShippingDateError =
        Error.Problem("Orders.InvalidShippingDateError", "Shipping date must be later than today's date");

    public static readonly Error NotUnpaid =
        Error.Problem("Orders.NoNotUnpaidtPending", "The order payment is not in unpaid status");

    public static readonly Error NotPending =
        Error.Problem("Orders.NotPending", "The order is not in pending status");

    public static readonly Error LineNotPending =
        Error.Problem("Orders.LineNotPending", "The order line is not in pending status");

    public static readonly Error NotStockReviewing =
        Error.Problem("Orders.NotStockReviewing", "The order is not in stock reviewing status");

    public static readonly Error NotShipped =
        Error.Problem("Orders.NotShipped", "The order is not in shipped status");

    public static readonly Error IsShipped =
        Error.Problem("Orders.IsShipped", "The order is in shipped status");

    public static readonly Error NotDelivered =
        Error.Problem("Orders.NotDelivered", "The order is not in delivered status");

    public static readonly Error IsDelivered =
        Error.Problem("Orders.IsDelivered", "The order is in delivered status");

    public static readonly Error PendingOrderLines =
        Error.Problem("Orders.PendingOrderLines", "Some order lines are still pending delivery");

    public static readonly Error NoDeliveryRecordFound =
        Error.Problem("Orders.NoDeliveryRecordFound", "No delivery record was found.");

    public static readonly Error NoShortage =
        Error.Problem("Orders.NoShortage", "There is no shortage for this product.");

    public static readonly Error InvalidQuantity =
        Error.Problem("Orders.InvalidQuantity", "The quantity must be greater than zero.");

    public static readonly Error InvalidDeliveryQuantity =
        Error.Problem("Orders.InvalidDeliveryQuantity", "The delivered quantity does not match the expected quantity after accounting for rejected items.");

    public static readonly Error MissingRejectReason =
        Error.Problem("Orders.MissingRejectReason", "A reject reason must be provided when the delivered quantity differs from the actual quantity.");

    public static readonly Error NoFilesUploaded =
        Error.Problem("Orders.NoFilesUploaded", "No files uploaded");

    public static readonly Error FileSizeExceeds =
        Error.Problem("Orders.FileSizeExceeds", "File size exceeds the 1 MB limit");

    public static Error InvalidFileType(List<string> _allowedExtensions) =>
        Error.Problem("Orders.InvalidFileType", $"Invalid file type. Only the following file types are allowed: {string.Join(", ", _allowedExtensions)}.");
}
