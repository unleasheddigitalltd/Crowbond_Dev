using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Domain.Locations;

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

    public static Error LocationNotEmpty(Guid locationId) =>
    Error.Conflict("Location.NotEmpty", $"The location with the identifier {locationId} was not empty");

    public static Error SettingNotFound() =>
    Error.Conflict("Setting.NotFound", $"Any active setting was not found");
}
