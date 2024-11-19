using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.OMS.Application.Orders.AddOrderLine;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.OMS.Presentation.Orders;

internal sealed class AddOrderLine : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("orders/{id}/lines", async (Guid id, Request request, ISender sender) =>
        {
            Result<Guid> result = await sender.Send(new AddOrderLineCommand(id, request.ProductId, request.Qty));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.ModifyOrders)
            .WithTags(Tags.Orders);
    }

    private sealed record Request(Guid ProductId, decimal Qty);
}
