using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.OMS.Application.PurchaseOrders.AddPurchaseOrderLine;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.OMS.Presentation.PurchaseOrders;

internal sealed class AddPurchaseOrderLine : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("purchase-orders/{id}/lines", async (Guid id, Request request, ISender sender) =>
        {
           var result = await sender.Send(new AddPurchaseOrderLineCommand(id, request.ProductId, request.UnitPrice, request.Qty, request.Comments));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.ModifyPurchaseOrders)
            .WithTags(Tags.PurchaseOrders);
    }

    public sealed record Request(Guid ProductId, decimal UnitPrice, decimal Qty, string? Comments);
}
