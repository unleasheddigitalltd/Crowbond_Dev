using Crowbond.Modules.OMS.Domain.PurchaseOrders;
using Crowbond.Modules.WMS.PublicApi;

namespace Crowbond.Modules.OMS.Application.Orders;

public sealed class InventoryService(
    IStockApi stockApi,
    IPurchaseOrderRepository purchaseOrderRepository)
{
    public async Task<decimal> GetAvailableQuantityAsync(Guid productId, CancellationToken cancellationToken = default)
    {
        List<PurchaseOrderLine> purchaseOrderLines = await purchaseOrderRepository.GetLinesPendingByProductAsync(productId, cancellationToken);
        decimal pendingQty = purchaseOrderLines.Sum(l => l.Qty);

        ProductStockSummaryResponse? response = await stockApi.GetSumByProductIdAsync(productId, cancellationToken);
        if (response is null)
        {
            return 0 + pendingQty;
        }

        return response.Qty + pendingQty;
    }
}
