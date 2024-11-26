using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.OMS.Application.Abstractions.Authentication;
using Crowbond.Modules.OMS.Application.Orders.CreateMyOrder;
using Crowbond.Modules.OMS.Application.Orders.CreateOrder;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.OMS.Presentation.Orders;

internal sealed class CreateOrder : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("orders", async (Request request, ISender sender) =>
        {
            Result<Guid> result = await sender.Send(new CreateOrderCommand(
                request.CustomerId,
                request.CustomerOutletId,
                request.ShippingDate,
                request.DeliveryMethod,
                request.PaymentMethod,
                request.CustomerComment));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .RequireAuthorization(Permissions.CreateOrders)
        .WithTags(Tags.Orders);
    }

    private sealed record Request(
    Guid CustomerId,
    Guid CustomerOutletId,
    DateOnly ShippingDate,
    int DeliveryMethod,
    int PaymentMethod,
    string? CustomerComment);
}
