using Crowbond.Common.Domain;

namespace Crowbond.Modules.OMS.Domain.Stocks;

public static class StockErrors
{
    public static Error ProductNotFound(Guid productId) =>
        Error.NotFound("Stocks.ProductNotFound",
            $"The stock for product with the identifier {productId} was not found");    
}
