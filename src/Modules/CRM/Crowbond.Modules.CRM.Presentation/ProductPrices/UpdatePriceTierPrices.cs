using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.CRM.Application.ProductPrices.UpdatePriceTierPrices;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.CRM.Presentation.ProductPrices;

internal sealed class UpdatePriceTierPrices : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("product-prices/price-tiers/{id}", async (Guid id, IReadOnlyCollection<ProductPriceRequest> request, ISender sender) =>
        {
            Result result = await sender.Send(new UpdatePriceTierPricesCommand(id, request));

            return result.Match(Results.NoContent, ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.ModifyPriceTiers)
            .WithTags(Tags.ProductPrices);
    }
}
