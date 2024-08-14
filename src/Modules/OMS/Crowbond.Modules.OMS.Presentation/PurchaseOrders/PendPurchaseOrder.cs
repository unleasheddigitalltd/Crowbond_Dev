using System.Security.Claims;
using Crowbond.Common.Domain;
using Crowbond.Common.Infrastructure.Authentication;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Crowbond.Modules.OMS.Application.PurchaseOrders.PendPurchaseOrder;

namespace Crowbond.Modules.OMS.Presentation.PurchaseOrders;

internal sealed class PendPurchaseOrder : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("purchase-order/{id}/pend", async (Guid id, ClaimsPrincipal claims, ISender sender) =>
        {
            Result result = await sender.Send(new PendPurchaseOrderCommand(claims.GetUserId(), id));

            return result.Match(Results.NoContent, ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.ModifyPurchaseOrders)
            .WithTags(Tags.PurchaseOrders);
    }
}
