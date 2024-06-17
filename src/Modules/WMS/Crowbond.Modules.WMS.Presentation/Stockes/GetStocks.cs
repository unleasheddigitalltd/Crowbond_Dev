using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.WMS.Application.Stocks.GetStocks;
using Crowbond.Modules.WMS.Application.Stocks.GetStocks.Dtos;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.WMS.Presentation.Stockes;

internal sealed class GetStocks : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("stocks", async (
            ISender sender,
            string search = "",
            string sort = "sku",
            string order = "asc",
            int page = 0,
            int size = 10
            ) =>
        {
            Result<StocksResponse> result = await sender.Send(
                new GetStocksQuery(search, sort, order, page, size));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .RequireAuthorization(Permissions.GetProducts)
        .WithTags(Tags.Stocks);
    }
}
