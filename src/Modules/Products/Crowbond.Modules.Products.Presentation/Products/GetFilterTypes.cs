using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.Products.Application.Products.GetFilterTypes.Dtos;
using Crowbond.Modules.Products.Application.Products.GetFilterTypes;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.Products.Presentation.Products;
internal sealed class GetFilterTypes : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("products/filter-types", async (ISender sender) =>
        {
            Result<IReadOnlyCollection<FilterTypeResponse>> result = await sender.Send(new GetFilterTypesQuery());

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .RequireAuthorization(Permissions.GetProducts)
        .WithTags(Tags.Products);
    }
}
