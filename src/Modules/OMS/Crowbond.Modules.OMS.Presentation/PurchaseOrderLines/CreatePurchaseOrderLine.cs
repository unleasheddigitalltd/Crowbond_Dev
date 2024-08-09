using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.OMS.Application.PurchaseOrderLines.CreatePurchaseOrderLine;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.OMS.Presentation.PurchaseOrderLines;

internal sealed class CreatePurchaseOrderLine : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("purchase-orders/{id}/lines", async (Guid id, Request request, ISender sender) =>
        {
            Result<Guid> result = await sender.Send(new CreatePurchaseOrderLineCommand(id, request.ProductId, request.Qty, request.Comments));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.ModifyPurchaseOrders)
            .WithTags(Tags.PurchaseOrders);
    }

    internal sealed record Request(Guid ProductId, decimal Qty, string? Comments);
}
