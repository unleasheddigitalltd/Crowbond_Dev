using System.Security.Claims;
using Crowbond.Common.Domain;
using Crowbond.Common.Infrastructure.Authentication;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.OMS.Application.PurchaseOrders.CreatePurchaseOrder;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.OMS.Presentation.PurchaseOrders;

internal sealed class CreatePurchaseOrder : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("purchase-orders", async (PurchaseOrderRequest request, ClaimsPrincipal claims, ISender sender) =>
        {
            Result<Guid> results = await sender.Send(new CreatePurchaseOrderCommand(claims.GetUserId(), request));

            return results.Match(Results.Ok, ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.CreatePurchaseOrders)
            .WithTags(Tags.PurchaseOrders);
    }
}
