using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Crowbond.Modules.Products.Application.Products.GetProducts;
using Crowbond.Modules.Products.Application.Products.GetProducts.Dto;

namespace Crowbond.Modules.Products.Presentation.Products;

internal sealed class GetProducts : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("products", async (ISender sender) =>
        {
            Result<ProductsResponse> result = await sender.Send(new GetProductQuery());

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .RequireAuthorization(Permissions.GetProducts)
        .WithTags(Tags.Products);
    }
}
