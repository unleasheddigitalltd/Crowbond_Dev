using Crowbond.Common.Domain;

namespace Crowbond.Modules.OMS.Domain.Orders;

public static class OrderErrors
{
    public static Error NotFound(Guid orderId) =>
        Error.NotFound("Orders.NotFound", $"The order with the identifier {orderId} was not found");

    public static Error LineNotFound(Guid orderLineId) =>
        Error.NotFound("Orders.LineNotFound", $"The order line with the identifier {orderLineId} was not found");
    
    public static Error DeliveryImageNotFound(string imageName) =>
        Error.NotFound("Orders.DeliveryImageNotFound", $"The order delivery image with the name {imageName} was not found");

    public static Error LineHasShortage(Guid orderLineId) =>
        Error.NotFound("Orders.LineHasShortage", $"The order line with the identifier {orderLineId} has shortage");

    public static Error NotAssignedToRouteTrip(Guid orderId) =>
        Error.NotFound("Orders.NotAssignedToRouteTrip", $"The order with the identifier {orderId} is not assigned to any route trip");

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

    public static readonly Error NotPending = 
        Error.Problem("Orders.NotPending", "The order is not in pending status");

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

    public static readonly Error NoDeliveryRecordFound = 
        Error.Problem("Orders.NoDeliveryRecordFound", "No delivery record was found.");

    public static readonly Error NoShortage = 
        Error.Problem("Orders.NoShortage", "There is no shortage for this product.");

    public static readonly Error InvalidOrderLineQuantity = 
        Error.Problem("Orders.InvalidOrderLineQuantity", "Order line quantity must be greater than zero.");
    
    public static readonly Error NoFilesUploaded = 
        Error.Problem("Orders.NoFilesUploaded", "No files uploaded");
    
    public static readonly Error FileSizeExceeds = 
        Error.Problem("Orders.FileSizeExceeds", "File size exceeds the 1 MB limit");
    
    public static Error InvalidFileType(List<string> _allowedExtensions) => 
        Error.Problem("Orders.InvalidFileType", $"Invalid file type. Only the following file types are allowed: {string.Join(", ", _allowedExtensions)}.");
}
