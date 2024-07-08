using Crowbond.Common.Domain;
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
        app.MapPost("PurchaseOrders", async (PurchaseOrderRequest request, ISender sender) =>
        {
            Result<Guid> result = await sender.Send(new CreatePurchaseOrderCommand(request));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .RequireAuthorization(Permission.CreatePurchaseOrders)
        .WithTags(Tags.PurchaseOrders);
    }
}
