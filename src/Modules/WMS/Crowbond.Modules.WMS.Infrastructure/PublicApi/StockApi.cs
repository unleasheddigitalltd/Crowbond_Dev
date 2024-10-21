using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Application.Stocks.GetProductStockSummary;
using Crowbond.Modules.WMS.PublicApi;
using MediatR;
using ProductStockSummaryResponse = Crowbond.Modules.WMS.PublicApi.ProductStockSummaryResponse;

namespace Crowbond.Modules.WMS.Infrastructure.PublicApi;

internal sealed class StockApi(ISender sender) : IStockApi
{
    public async Task<ProductStockSummaryResponse?> GetSumByProductIdAsync(Guid productId, CancellationToken cancellationToken = default)
    {
        Result<Application.Stocks.GetProductStockSummary.ProductStockSummaryResponse> result = 
            await sender.Send(new GetProductStockSummaryQuery(productId), cancellationToken);

        if (result.IsFailure)
        {
            return null;
        }

        return new ProductStockSummaryResponse(result.Value.Qty);
    }
}
