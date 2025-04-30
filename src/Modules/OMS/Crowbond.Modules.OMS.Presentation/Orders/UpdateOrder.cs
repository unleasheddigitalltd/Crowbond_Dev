using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.OMS.Application.Orders.UpdateOrder;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.OMS.Presentation.Orders;
internal sealed class UpdateOrder : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("orders/{id}", async (Guid id, Request request, ISender sender) =>
        {
            var result = await sender.Send(new UpdateOrderCommand(
                id,
                request.CustomerOutletId,
                request.ShippingDate,
                request.DeliveryMethod,
                request.PaymentMethod,
                request.CustomerComment));

            return result.Match(() => Results.Ok(), ApiResults.Problem);
        })
        .RequireAuthorization(Permissions.ModifyOrders)
        .WithTags(Tags.Orders);
    }

    private sealed record Request(
        Guid CustomerOutletId,
        DateOnly ShippingDate,
        int DeliveryMethod,
        int PaymentMethod,
        string? CustomerComment);
}
