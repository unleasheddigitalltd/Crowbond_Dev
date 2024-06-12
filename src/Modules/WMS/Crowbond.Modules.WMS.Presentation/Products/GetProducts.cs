using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Crowbond.Modules.WMS.Application.Products.GetProducts;
using Crowbond.Modules.WMS.Application.Products.GetProducts.Dto;

namespace Crowbond.Modules.WMS.Presentation.Products;

internal sealed class GetProducts : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("products", async (
            ISender sender,
            string search = "",
            string sort = "name",
            string order = "asc",
            int page = 1,
            int size = 10
            ) =>
        {
            Result<ProductsResponse> result = await sender.Send(
                new GetProductQuery(search, sort, order, page, size));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .RequireAuthorization(Permissions.GetProducts)
        .WithTags(Tags.Products);
    }
}
