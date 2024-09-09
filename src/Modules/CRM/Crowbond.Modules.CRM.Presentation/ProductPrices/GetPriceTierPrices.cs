using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.CRM.Application.ProductPrices.GetPriceTierPrices;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.CRM.Presentation.ProductPrices;

internal sealed class GetPriceTierPrices : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("price-tiers/{id}", async (Guid id, ISender sender) =>
        {
            Result<IReadOnlyCollection<ProductPriceResponse>> result = await sender.Send(new GetPriceTierPricesQuery(id));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.GetPriceTiers)
            .WithTags(Tags.PriceTiers);
    }
}
