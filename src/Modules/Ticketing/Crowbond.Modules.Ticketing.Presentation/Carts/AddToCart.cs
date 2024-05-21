using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.Ticketing.Application.Abstractions.Authentication;
using Crowbond.Modules.Ticketing.Presentation;
using Crowbond.Modules.Ticketing.Application.Carts.AddItemToCart;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.Ticketing.Presentation.Carts;

internal sealed class AddToCart : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("carts/add", async (Request request, ICustomerContext customerContext, ISender sender) =>
        {
            Result result = await sender.Send(
                new AddItemToCartCommand(
                    customerContext.CustomerId,
                    request.TicketTypeId,
                    request.Quantity));

            return result.Match(() => Results.Ok(), ApiResults.Problem);
        })
        .RequireAuthorization(Permissions.AddToCart)
        .WithTags(Tags.Carts);
    }

    internal sealed class Request
    {
        public Guid TicketTypeId { get; init; }

        public decimal Quantity { get; init; }
    }
}
