using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.WMS.Application.Products.GetUnitOfMeasures;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Crowbond.Modules.WMS.Application.Products.GetUnitOfMeasures.Dtos;

namespace Crowbond.Modules.WMS.Presentation.Products;

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
