using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.OMS.Application.Abstractions.Authentication;
using Crowbond.Modules.OMS.Application.Orders.CreateMyOrder;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.OMS.Presentation.Orders;

internal sealed class CreateMyOrder : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("orders", async (IContactContext contactContext, Request request, ISender sender) =>
        {
            Result result = await sender.Send(new CreateMyOrderCommand(
                contactContext.ContactId,
                request.CustomerOutletId,
                request.ShippingDate, 
                request.DeliveryMethod,
                request.PaymentMethod,
                request.CustomerComment));

            return result.Match(() => Results.Ok(), ApiResults.Problem);
        })
        .RequireAuthorization(Permissions.CreateOrder)
        .WithTags(Tags.Orders);
    }

    private sealed record Request(
        Guid CustomerOutletId,
        DateOnly ShippingDate,
        int DeliveryMethod,
        int PaymentMethod,
        string? CustomerComment);
}
