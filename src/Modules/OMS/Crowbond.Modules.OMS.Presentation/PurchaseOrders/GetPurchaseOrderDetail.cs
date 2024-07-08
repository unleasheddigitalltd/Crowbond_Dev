using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.OMS.Application.PurchaseOrders.GetPurchaseOrderDetails;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.OMS.Presentation.PurchaseOrders;

internal sealed class GetPurchaseOrderDetail : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("purchaseorders/{id}/details", async (Guid id, ISender sender) =>
        {
            Result<PurchaseOrderDetailsResponse> result = await sender.Send(new GetPurchaseOrderDetailsQuery(id));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .RequireAuthorization(Permissions.GetPurchaseOrders)
        .WithTags(Tags.PurchaseOrders);
    }
}
