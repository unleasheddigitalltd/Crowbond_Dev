using Crowbond.Common.Domain;

namespace Crowbond.Modules.WMS.Domain.Stocks;
public static class StockErrors
{
    public static Error NotFound(Guid stockId) =>
    Error.NotFound("Stocks.NotFound", $"The stock with the identifier {stockId} was not found");

    public static Error ReasonNotFound(Guid reasonId) =>
    Error.NotFound("StockTransactionReasons.NotFound", $"The reason with the identifier {reasonId} was not found");

    public static Error LocationNotFound(Guid locationId) =>
    Error.NotFound("Location.NotFound", $"The location with the identifier {locationId} was not found");

    public static Error ProductNotFound(Guid productId) =>
    Error.NotFound("Product.NotFound", $"The product with the identifier {productId} was not found");

    public static Error ReceiptNotFound(Guid receiptId) =>
    Error.NotFound("Receipt.NotFound", $"The receipt with the identifier {receiptId} was not found");

    public static Error StatusNotFound(string stockStatus) =>
    Error.NotFound("Stocks.StatusNotFound", $"The status {stockStatus} was not found");

    public static Error LocationNotEmpty(Guid locationId) =>
    Error.Conflict("Location.NotEmpty", $"The location with the identifier {locationId} was not empty");

    public static readonly Error SettingNotFound = Error.Conflict("Setting.NotFound", $"Any active setting was not found");

    public static readonly Error NotActive = Error.Problem("Stocks.NotActive", "The stock is not in active status");

    public static readonly Error NotHeld = Error.Problem("Stocks.NotHeld", "The stock is not in held status");

    public static readonly Error NonPositiveQty = Error.Problem("Stocks.NonPositiveQty", "The quantity must be greater than zero");

    public static readonly Error OverdrawnStock = Error.Problem("Stocks.OverdrawnStock", "Cannot remove more stock than is currently available.");

    public static readonly Error SameLocation = Error.Conflict("Stock.Same Location", $"The destination and origin are the same location.");
}
