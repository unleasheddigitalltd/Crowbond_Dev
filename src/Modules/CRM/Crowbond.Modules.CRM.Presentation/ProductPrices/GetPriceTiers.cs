using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.CRM.Application.ProductPrices.GetPriceTiers;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.CRM.Presentation.ProductPrices;

internal sealed class GetPriceTiers : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("product-prices/price-tiers", async (ISender sender) =>
        {
            Result<IReadOnlyCollection<PriceTierResponse>> result = await sender.Send(new GetPriceTiersQuery());

            return result.Match(Results.Ok, ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.GetCustomers)
            .WithTags(Tags.ProductPrices);
    }
}
