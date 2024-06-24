using Crowbond.Common.Domain;

namespace Crowbond.Modules.WMS.Domain.Stocks;
public static class StockErrors
{
    public static Error NotFound(Guid stockId) =>
    Error.NotFound("Stocks.NotFound", $"The stock with the identifier {stockId} was not found");

    public static Error ReasonNotFound(Guid reasonId) =>
    Error.NotFound("StockTransactionReasons.NotFound", $"The reason with the identifier {reasonId} was not found");
}
