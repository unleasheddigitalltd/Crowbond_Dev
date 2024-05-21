using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.Ticketing.Application.Abstractions.Authentication;
using Crowbond.Modules.Ticketing.Presentation;
using Crowbond.Modules.Ticketing.Application.Carts.RemoveItemFromCart;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.Ticketing.Presentation.Carts;

internal sealed class RemoveFromCart : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("carts/remove", async (Request request, ICustomerContext customerContext, ISender sender) =>
        {
            Result result = await sender.Send(
                new RemoveItemFromCartCommand(customerContext.CustomerId, request.TicketTypeId));

            return result.Match(Results.NoContent, ApiResults.Problem);
        })
        .RequireAuthorization(Permissions.RemoveFromCart)
        .WithTags(Tags.Carts);
    }

    internal sealed class Request
    {
        public Guid TicketTypeId { get; init; }
    }
}
