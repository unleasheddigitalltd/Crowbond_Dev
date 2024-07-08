using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.OMS.Application.PurchaseOrders.GetPurchaseOrder;
using Crowbond.Modules.OMS.Application.PurchaseOrders.GetPurchaseOrders;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.OMS.Presentation.PurchaseOrders;

internal sealed class GetPurchaseOrder : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("purchaseorders/{id}", async (Guid id, ISender sender) =>
        {
            Result<PurchaseOrderResponse> result = await sender.Send(new GetPurchaseOrderQuery(id));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .RequireAuthorization(Permission.GetPurchaseOrders)
        .WithTags(Tags.PurchaseOrders);
    }
}
