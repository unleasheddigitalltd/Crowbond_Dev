using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.WMS.Application.Stocks.GetStocksByProduct;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.WMS.Presentation.Stocks;

internal sealed class GetStocksByProduct : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("stocks/products/{id}", async (Guid id, ISender sender) =>
        {
            Result<IReadOnlyCollection<StockResponse>> result = await sender.Send(new GetStocksByProductQuery(id));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.GetStocks)
            .WithTags(Tags.Stocks);
    }
}
