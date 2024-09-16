using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.OMS.Application.Abstractions.Authentication;
using Crowbond.Modules.OMS.Application.Carts.AddItemToCart;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.OMS.Presentation.Carts;

internal sealed class AddItemToCart : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("carts/add", async (Request request, IContactContext contactContext, ISender sender) =>
        {
            Result results = await sender.Send(
                new AddItemToCartCommand(
                    contactContext.ContactId,
                    request.ProductId,
                    request.Quantity));

            return results.Match(() => Results.Ok(), ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.AddToCart)
            .WithTags(Tags.Carts);
    }

    private sealed record Request(Guid ProductId, decimal Quantity);
}
