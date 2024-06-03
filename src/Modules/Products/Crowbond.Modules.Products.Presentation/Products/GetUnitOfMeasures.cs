using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.Products.Application.Products.GetUnitOfMeasures.Dtos;
using Crowbond.Modules.Products.Application.Products.GetUnitOfMeasures;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Crowbond.Modules.Products.Application.Products.GetInventoryTypes.Dtos;
using Crowbond.Modules.Products.Application.Products.GetInventoryTypes;

namespace Crowbond.Modules.Products.Presentation.Products;

internal sealed class GetUnitOfMeasures : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("products/unit-of-measures", async (ISender sender) =>
        {
            Result<IReadOnlyCollection<UnitOfMeasureResponse>> result = await sender.Send(new GetUnitOfMeasuresQuery());

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .RequireAuthorization(Permissions.GetProducts)
        .WithTags(Tags.Products);
    }
}
