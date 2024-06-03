using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.Products.Application.Products.GetInventoryTypes.Dtos;
using Crowbond.Modules.Products.Application.Products.GetInventoryTypes;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.Products.Presentation.Products;
internal sealed class GetInventoryTypes : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("products/inventory-types", async (ISender sender) =>
        {
            Result<IReadOnlyCollection<InventoryTypeResponse>> result = await sender.Send(new GetInventoryTypesQuery());

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .RequireAuthorization(Permissions.GetProducts)
        .WithTags(Tags.Products);
    }
}
