using System.Security.Claims;
using Crowbond.Common.Domain;
using Crowbond.Common.Infrastructure.Authentication;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.OMS.Application.PurchaseOrders.UpdatePurchaseOrderDetails;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.OMS.Presentation.PurchaseOrders;

internal sealed class UpdatePurchaseOrderDetails : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("purchase-orders/{id}/details", async (Guid id, PurchaseOrderDetailsRequest request, ClaimsPrincipal claims, ISender sender) =>
        {
            Result result = await sender.Send(new UpdatePurchaseOrderDetailsCommand(id, claims.GetUserId(), request));

            return result.Match(Results.NoContent, ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.ModifyPurchaseOrders)
            .WithTags(Tags.PurchaseOrders);
    }
}
