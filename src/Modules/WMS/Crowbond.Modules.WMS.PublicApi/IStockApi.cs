namespace Crowbond.Modules.WMS.PublicApi;

public interface IStockApi
{
    Task<ProductStockSummaryResponse?> GetSumByProductIdAsync(Guid productId, CancellationToken cancellationToken = default);
}

public sealed record ProductStockSummaryResponse(decimal Qty);
