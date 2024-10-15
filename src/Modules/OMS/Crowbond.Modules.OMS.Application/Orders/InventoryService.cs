using Crowbond.Modules.WMS.PublicApi;

namespace Crowbond.Modules.OMS.Application.Orders;

public sealed class InventoryService(IStockApi stockApi)
{
    public async Task<decimal> GetAvailableQuantityAsync(Guid productId, CancellationToken cancellationToken = default)
    {
        ProductStockSummaryResponse? response = await stockApi.GetSumByProductIdAsync(productId, cancellationToken);
        if (response is null)
        {
            return 0;
        }

        return response.Qty;
    }
}
