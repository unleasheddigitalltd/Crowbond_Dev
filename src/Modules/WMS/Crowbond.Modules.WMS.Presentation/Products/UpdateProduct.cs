using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.WMS.Application.Products.UpdateProduct;
using Crowbond.Modules.WMS.Application.Products.UpdateProduct.Dtos;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.WMS.Presentation.Products;

internal sealed class UpdateProduct : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("products/{id}", async (Guid id, ProductDto request, ISender sender) =>
        {
            Result result = await sender.Send(new UpdateProductCommand(id, request));

            return result.Match(Results.NoContent, ApiResults.Problem);
        })
        .RequireAuthorization(Permissions.ModifyProducts)
        .WithTags(Tags.Products);
    }
}
