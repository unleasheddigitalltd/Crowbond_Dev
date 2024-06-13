using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.WMS.Application.Products.GetProductStocks;
using Crowbond.Modules.WMS.Application.Products.GetProductStocks.Dtos;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.WMS.Presentation.Products;

internal sealed class GetProductStocks : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("products/{id}/stocks", async (Guid id, ISender sender) =>
        {
            Result<IReadOnlyCollection<StockResponse>> result = await sender.Send(new GetProductStocksQuery(id));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .RequireAuthorization(Permissions.GetProducts)
        .WithTags(Tags.Products);
    }
}
